using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Models.Interfaces
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file, string description);
        SearchResult SearchPhoto (string paramsk);
    }
}