using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using Ingenico.Barcode.Domain.Entites;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ingenico.Barcode.Shared.Exceptions;
using Ingenico.Barcode.Shared.Enums;

namespace Ingenico.Barcode.Domain.Handlers
{
    public class ObterProdutoRequestHandler : IRequestHandler<ObterProdutoRequest, Result<ObterProdutoResponse>>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ObterProdutoRequestHandler> _logger;

        public ObterProdutoRequestHandler(IProdutoRepository produtoRepository, ILogger<ObterProdutoRequestHandler> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        public async Task<Result<ObterProdutoResponse>> Handle(ObterProdutoRequest request, CancellationToken cancellationToken)
        {
            // Carregando o produto junto com as categorias e tags associadas
            var produto = await _produtoRepository
                .ObterQueryable()
                .Include(p => p.ProdutoCategoria)
                    .ThenInclude(pc => pc.Categoria)
                .Include(p => p.ProdutoTag)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.ProdutoId == request.ProdutoId);

            if (produto == null)
            {
                _logger.LogWarning("Produto não encontrado: {ProdutoId}", request.ProdutoId);
                return Result.Error<ObterProdutoResponse>(new ExceptionAplication(AuthError.UsuarioNaoEncontrado));
            }

            var response = new ObterProdutoResponse
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Marca = produto.Marca,
                Peso = produto.Peso,
                Preco = produto.Preco,
                UnidadeMedida = produto.UnidadeMedida,
                Ingredientes = produto.Ingredientes,
                PaisOrigem = produto.PaisOrigem,
                DataFabricacao = produto.DataFabricacao,
                Lote = produto.Lote,
                Validade = produto.Validade,
                Categorias = produto.ProdutoCategoria.Select(pc => new ObterCategoriaResponse
                {
                    CategoriaId = pc.Categoria.CategoriaId,
                    Nome = pc.Categoria.Nome
                }).ToList(),
                Tags = produto.ProdutoTag.Select(pt => new ObterTagResponse
                {
                    TagId = pt.Tag.TagId,
                    NomeTag = pt.Tag.Nome
                }).ToList()
            };

            return Result.Success(response);
        }
    }
}
