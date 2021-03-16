using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models
{
    public class Add
    {
        public class Command : IRequest<Models.Photo>
        {
            public IFormFile File { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Photo>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAcessor;
            public Handler(DataContext context, IPhotoAccessor photoAcessor)
            {
                _photoAcessor = photoAcessor;
                _context = context;
            }

            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = _photoAcessor.AddPhoto(request.File, request.Description);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    Description = request.Description,
                };

                _context.Photos.Add(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return photo;

                throw new Exception("Problem saving changes");
            }


        }
    }
}