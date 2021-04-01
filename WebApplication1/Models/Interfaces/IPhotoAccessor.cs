using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication1.Models.Interfaces
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file, string description);
        SearchResult SearchPhoto (string paramsk);
        Task<string> DeletePhoto(string publicId);
    }
}