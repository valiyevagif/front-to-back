using Microsoft.AspNetCore.Http;

namespace Bigon.Infrastructure.Services.Abstracts
{
    public interface IFileService
    {
        string Upload(IFormFile file);
        string ChangeFile(IFormFile file, string oldFileName, bool withoutArchive = false);
    }
}
