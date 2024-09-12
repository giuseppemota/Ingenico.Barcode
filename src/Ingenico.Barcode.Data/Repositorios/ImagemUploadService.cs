using Ingenico.Barcode.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ingenico.Barcode.Data.Repositorios;

public class ImageUploadService : IImageUploadService {
    private readonly string _imageFolderPath;

    public ImageUploadService(IConfiguration configuration) {
        // Lê o caminho do appsettings.json
        _imageFolderPath = configuration["ImageSettings:ImageFolderPath"];
    }

    public string UploadImage(IFormFile image) {
        if (image == null || image.Length == 0)
            throw new ArgumentException("Imagem inválida.");

        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var imagePath = Path.Combine(_imageFolderPath, imageName);

        using (var stream = new FileStream(imagePath, FileMode.Create)) {
            image.CopyTo(stream);
        }

        return imageName; // Retorna o nome do arquivo para salvar no banco
    }
}
