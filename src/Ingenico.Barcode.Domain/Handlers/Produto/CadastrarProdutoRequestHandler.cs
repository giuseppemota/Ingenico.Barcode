using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using Ingenico.Barcode.Domain.Entites;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;


namespace Ingenico.Barcode.Domain.Handlers{
    public class CadastrarProdutoRequestHandler : IRequestHandler<CadastrarProdutoRequest, Result<CadastrarProdutoResponse>> {

        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CadastrarProdutoRequestHandler> _logger;

        public CadastrarProdutoRequestHandler(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            ILogger<CadastrarProdutoRequestHandler> logger) {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<CadastrarProdutoResponse>> Handle(CadastrarProdutoRequest request, CancellationToken cancellationToken) {

            var produto = new ProdutoEntity {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Marca = request.Marca,
                Validade = request.Validade,
                Peso = request.Peso,
                UnidadeMedida = request.UnidadeMedida,
                Ingredientes = request.Ingredientes,
                PaisOrigem = request.PaisOrigem
            };

            await _produtoRepository.CadastrarProdutoAsync(produto);

            var categoria = new CategoriaEntity {
                Nome = request.Categoria.Nome,
                Produto = produto
            };
            var _categoria = await _categoriaRepository.CadastrarCategoriaAsync(categoria);

            var tag = new TagEntity {
                NomeTag = request.Tag.NomeTag,
                Produto = produto
            };
            var _tag = await _tagRepository.CadastrarTagAsync(tag);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Produto cadastrado com categoria e tag");

            return Result.Success(new CadastrarProdutoResponse {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Marca = produto.Marca,
                Peso = produto.Peso,
                Preco = produto.Preco,
                UnidadeMedida = produto.UnidadeMedida,
                Ingredientes = produto.Ingredientes,
                PaisOrigem = produto.PaisOrigem,
                Categoria = new CadastrarCategoriaResponse {
                    CategoriaId = _categoria.CategoriaId,
                    Nome = _categoria.Nome,
                    IdProduto = _categoria.IdProduto
                },
                Tag = new CadastrarTagResponse {
                    TagId = _tag.TagId,
                    NomeTag = _tag.NomeTag,
                    IdProduto = _tag.IdProduto
                }
            }) ;
        }
    }
}
