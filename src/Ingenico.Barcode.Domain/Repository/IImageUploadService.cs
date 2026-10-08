using Microsoft.AspNetCore.Http;

namespace Ingenico.Barcode.Domain.Repository {
    public interface IImageUploadService {
        Task<string?> UploadImageAsync(IFormFile? image, CancellationToken cancellationToken);
        Task<byte[]?> GetImageDataAsync(string imagePath, CancellationToken cancellationToken);
    }
}
