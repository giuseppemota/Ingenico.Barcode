using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers {
    public class ObterTodosProdutosRequestHandler : IRequestHandler<ObterTodosProdutosRequest, Result<ObterTodosProdutosResponse>> {
        private readonly IProdutoRepository _produtoRepository;

        private readonly ILogger<ObterTodosProdutosRequestHandler> _logger;

        public ObterTodosProdutosRequestHandler(
            IProdutoRepository produtoRepository,

            ILogger<ObterTodosProdutosRequestHandler> logger) {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        public async Task<Result<ObterTodosProdutosResponse>> Handle(ObterTodosProdutosRequest request, CancellationToken cancellationToken) {
            var produtos = await _produtoRepository.ObterTodosProdutosAsync();
            if (produtos == null) {
                _logger.LogError("Nenhuma pessoa encontrada");
                //return Result.Error<ObterTodasPessoasResponse>(new Compartilhado.Exceptions.SemResultadosExcecao());
            }

            var produtoResponses = new List<ObterProdutoResponse>();

            foreach (var produto in produtos) { 

                produtoResponses.Add(new ObterProdutoResponse {
                    ProdutoId = produto.ProdutoId,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Marca = produto.Marca,
                    Validade = produto.Validade,
                    Peso = produto.Peso,
                    Preco = produto.Preco,
                    UnidadeMedida = produto.UnidadeMedida,
                    Ingredientes = produto.Ingredientes,
                    PaisOrigem = produto.PaisOrigem
                });
            }
            var response = new ObterTodosProdutosResponse {
                Produtos = produtoResponses
            };

            _logger.LogInformation("Retornando lista de produtos");

            return Result.Success(response);
        }
    }
}
