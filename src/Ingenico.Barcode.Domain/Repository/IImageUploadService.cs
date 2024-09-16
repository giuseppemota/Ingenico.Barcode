using Microsoft.AspNetCore.Http;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IImageUploadService {
        string UploadImage(IFormFile image);
    }
}
