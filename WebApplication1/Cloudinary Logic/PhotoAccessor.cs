using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebApplication1.Models;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;
        public PhotoAccessor(IOptions<CloudinarySettings> config, DataContext context)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
            _context = context;
        }

        public PhotoUploadResult AddPhoto(IFormFile file, string description)
        {
            var uploadResult = new ImageUploadResult();
            
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                        Tags = description
                    };
                    
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUri.AbsoluteUri
            };
        }

        public SearchResult SearchPhoto(string paramsk)
        {
            SearchResult result = _cloudinary.Search()
            .Expression(paramsk)
            .WithField("context")
            .WithField("tags")
            .MaxResults(10)
            .Execute();

            return result;
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = _cloudinary.Destroy(deleteParams); // remove from cloud

            var photo = await _context.Photos.FindAsync(publicId);
            _context.Photos.Remove(photo); // remove from local db
            _context.SaveChanges();

            return result.Result == "ok" ? result.Result : null;
        }
    }
}