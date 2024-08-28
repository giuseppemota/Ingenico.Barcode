using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;


namespace Ingenico.Barcode.Domain.Handlers{
    public class ObterProdutoRequestHandler : IRequestHandler<ObterProdutoRequest, Result<ObterProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ObterProdutoRequestHandler> _logger;

        public ObterProdutoRequestHandler(
            IProdutoRepository produtoRepository,

            ILogger<ObterProdutoRequestHandler> logger) {
            _produtoRepository = produtoRepository;

            _logger = logger;
        }

        public async Task<Result<ObterProdutoResponse>> Handle(ObterProdutoRequest request, CancellationToken cancellationToken) {
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);
            if (produto is null) {
                _logger.LogError("Produto nao encontrado.");
                //return Result.Error<ObterPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            var produtoResponse = new ObterProdutoResponse {

                
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Marca = produto.Marca,
                Peso = produto.Peso,
                Preco = produto.Preco,
                UnidadeMedida = produto.UnidadeMedida,
                Ingredientes = produto.Ingredientes,
                PaisOrigem = produto.PaisOrigem,

            };

            _logger.LogInformation("Retornando Produto encontrado.");

            return Result.Success(produtoResponse);
        }

        private ObterCategoriaResponse MapToCadastrarCategoriaResponse(CategoriaEntity categoria) {

            return new ObterCategoriaResponse { 
                
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                IdProduto = categoria.IdProduto

            };
        }
        private ObterTagResponse MapToCadastrarTagResponse(TagEntity tag) {

            return new ObterTagResponse {
                TagId = tag.TagId,
                NomeTag = tag.NomeTag,
                IdProduto = tag.IdProduto,

            };
        }
    }
}
