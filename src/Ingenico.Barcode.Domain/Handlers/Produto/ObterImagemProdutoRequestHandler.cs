using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers {
    public class ObterImagemProdutoRequestHandler : IRequestHandler<ObterImagemProdutoRequest, Result<ObterImagemProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;

        public ObterImagemProdutoRequestHandler(IProdutoRepository produtoRepository) {
            _produtoRepository = produtoRepository;
        }

        public async Task<Result<ObterImagemProdutoResponse>> Handle(ObterImagemProdutoRequest request, CancellationToken cancellationToken) {
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);

            if (produto == null || string.IsNullOrEmpty(produto.ImagePath)) {
                return new ObterImagemProdutoResponse {
                    ImagePath = null // Devolve null se não houver imagem ou produto
                };
            }

            return new ObterImagemProdutoResponse {
                ImagePath = produto.ImagePath
            };
        }
    }
}
