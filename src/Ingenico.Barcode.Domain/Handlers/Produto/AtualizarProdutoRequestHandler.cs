using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers {
    public class AtualizarProdutoRequestHandler : IRequestHandler<AtualizarProdutoRequest, Result<AtualizarProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AtualizarProdutoRequestHandler> _logger;

        public AtualizarProdutoRequestHandler(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, ILogger<AtualizarProdutoRequestHandler> logger) {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<AtualizarProdutoResponse>> Handle(AtualizarProdutoRequest request, CancellationToken cancellationToken) {
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);

            produto.Atualizar(request);
            var produtoAtualizado = await _produtoRepository.AtualizarProdutoAsync(produto);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Produto atualizado");

            var response = new AtualizarProdutoResponse {

                ProdutoId = produtoAtualizado.ProdutoId,
                Nome = produtoAtualizado.Nome,
                Descricao = produtoAtualizado.Descricao,
                Marca = produtoAtualizado.Marca,
                Validade = produtoAtualizado.Validade,
                Peso = produtoAtualizado.Peso,
                Preco = produtoAtualizado.Preco,
                UnidadeMedida = produtoAtualizado.UnidadeMedida,
                Ingredientes = produtoAtualizado.Ingredientes,
                PaisOrigem = produtoAtualizado.PaisOrigem
            };

            return Result.Success(response);
        }

    }
}
