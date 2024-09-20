using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.IO;

namespace Ingenico.Barcode.Domain.Handlers {
    public class ObterImagemProdutoRequestHandler : IRequestHandler<ObterImagemProdutoRequest, Result<ObterImagemProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IImageUploadService _imageUploadService;

        public ObterImagemProdutoRequestHandler(
            IProdutoRepository produtoRepository,
            IImageUploadService imageUploadService) {
            _produtoRepository = produtoRepository;
            _imageUploadService = imageUploadService;
        }

        public async Task<Result<ObterImagemProdutoResponse>> Handle(ObterImagemProdutoRequest request, CancellationToken cancellationToken) {
            // Busca o produto pelo Id
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);

            if (produto == null || string.IsNullOrEmpty(produto.ImagePath)) {
                return Result.Error<ObterImagemProdutoResponse>(new Exception("Imagem não encontrada"));
            }

            // Obtem o caminho da imagem
            var imagePath = produto.ImagePath;
            var imageData = _imageUploadService.GetImageData(imagePath); // Método que retorna os bytes da imagem

            return Result.Success( new ObterImagemProdutoResponse {
                ImagePath = imagePath,
                ImageData = imageData
            });
        }
    }


}
