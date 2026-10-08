using Ingenico.Barcode.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Ingenico.Barcode.Data.Repositorios;

public class ImageUploadService : IImageUploadService {
    private readonly Cloudinary _cloudinary;
    private readonly HttpClient _httpClient;
    private readonly string _cloudName;

    public ImageUploadService(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
        _cloudName = configuration["Cloudinary:CloudName"]
            ?? throw new InvalidOperationException("Cloudinary:CloudName must be configured.");
        var apiKey = configuration["Cloudinary:ApiKey"]
            ?? throw new InvalidOperationException("Cloudinary:ApiKey must be configured.");
        var apiSecret = configuration["Cloudinary:ApiSecret"]
            ?? throw new InvalidOperationException("Cloudinary:ApiSecret must be configured.");

        _cloudinary = new Cloudinary(new Account(_cloudName, apiKey, apiSecret));
        _cloudinary.Api.Secure = true;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string?> UploadImageAsync(IFormFile? image, CancellationToken cancellationToken) {
        if (image == null || image.Length == 0)
            return null;

        await using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams {
            File = new FileDescription(image.FileName, stream)
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        if (uploadResult.Error is not null) {
            throw new InvalidOperationException($"Cloudinary image upload failed: {uploadResult.Error.Message}");
        }

        return uploadResult.SecureUrl?.ToString()
            ?? throw new InvalidOperationException("Cloudinary did not return a secure image URL.");
    }

    public async Task<byte[]?> GetImageDataAsync(string imagePath, CancellationToken cancellationToken) {
        var expectedPathPrefix = $"/{_cloudName}/image/upload/";
        if (!Uri.TryCreate(imagePath, UriKind.Absolute, out var imageUri)
            || imageUri.Scheme != Uri.UriSchemeHttps
            || imageUri.Host != "res.cloudinary.com"
            || !imageUri.AbsolutePath.StartsWith(expectedPathPrefix, StringComparison.Ordinal)) {
            throw new InvalidOperationException("Stored image URL is not a Cloudinary URL for the configured account.");
        }

        using var response = await _httpClient.GetAsync(
            imageUri,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync(cancellationToken);
    }
}
