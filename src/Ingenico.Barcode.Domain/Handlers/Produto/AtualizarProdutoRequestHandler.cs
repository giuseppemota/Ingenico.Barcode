using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using Ingenico.Barcode.Domain.Entites;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Ingenico.Barcode.Shared.Exceptions;
using Ingenico.Barcode.Shared.Enums;

namespace Ingenico.Barcode.Domain.Handlers
{
        public class AtualizarProdutoRequestHandler : IRequestHandler<AtualizarProdutoRequest, Result<AtualizarProdutoResponse>>
        {
            private readonly IProdutoRepository _produtoRepository;
            private readonly ICategoriaRepository _categoriaRepository;
            private readonly ITagRepository _tagRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IImageUploadService _imageUploadService;
            private readonly ILogger<AtualizarProdutoRequestHandler> _logger;

            public AtualizarProdutoRequestHandler(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            ILogger<AtualizarProdutoRequestHandler> logger,
            IImageUploadService imageUploadService)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageUploadService = imageUploadService;
        }

        public async Task<Result<AtualizarProdutoResponse>> Handle(AtualizarProdutoRequest request, CancellationToken cancellationToken) {
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);
            string? imagePath = produto.ImagePath;

            if (request.Image != null)
            {
                // Realiza o upload e salva o caminho
                imagePath = _imageUploadService.UploadImage(request.Image);
            }
            if (produto == null) {
                _logger.LogWarning("Produto não encontrado: {ProdutoId}", request.ProdutoId);
                return Result.Error<AtualizarProdutoResponse>(new ExceptionAplication(AuthError.UsuarioNaoEncontrado));
            }

            // Atualizar propriedades básicas
            produto.Nome = request.Nome;
            produto.Descricao = request.Descricao;
            produto.Marca = request.Marca;
            produto.Lote = request.Lote;
            produto.DataFabricacao = request.DataFabricacao;
            produto.Validade = request.Validade;
            produto.Preco = request.Preco;
            produto.Peso = request.Peso;
            produto.UnidadeMedida = request.UnidadeMedida;
            produto.Ingredientes = request.Ingredientes;
            produto.PaisOrigem = request.PaisOrigem;
            produto.ImagePath = imagePath;

            // Remover associações antigas de categorias
            _produtoRepository.RemoverCategorias(produto);

            // Atualizar categorias
            produto.ProdutoCategoria.Clear();
            foreach (var categoriaRequest in request.Categorias) {
                var categoria = await _categoriaRepository.ObterCategoriaPorNomeAsync(categoriaRequest.Nome);
                if (categoria == null) {
                    categoria = new CategoriaEntity {
                        Nome = categoriaRequest.Nome
                    };
                    await _categoriaRepository.CadastrarCategoriaAsync(categoria);
                }

                produto.ProdutoCategoria.Add(new ProdutoCategoria {
                    Categoria = categoria
                });
            }

            // Remover associações antigas de tags
            _produtoRepository.RemoverTags(produto);

            // Atualizar tags
            produto.ProdutoTag.Clear();
            foreach (var tagRequest in request.Tags) {
                var tag = await _tagRepository.ObterTagPorNomeAsync(tagRequest.Nome);
                if (tag == null) {
                    tag = new TagEntity {
                        Nome = tagRequest.Nome
                    };
                    await _tagRepository.CadastrarTagAsync(tag);
                }

                produto.ProdutoTag.Add(new ProdutoTag {
                    Tag = tag
                });
            }

            await _produtoRepository.AtualizarProdutoAsync(produto);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Produto atualizado com categorias e tags");

            return Result.Success(new AtualizarProdutoResponse {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Marca = produto.Marca,
                Lote = produto.Lote,
                DataFabricacao = produto.DataFabricacao,
                Validade = produto.Validade,
                Peso = produto.Peso,
                Preco = produto.Preco,
                UnidadeMedida = produto.UnidadeMedida,
                Ingredientes = produto.Ingredientes,
                PaisOrigem = produto.PaisOrigem,
                Categorias = produto.ProdutoCategoria.Select(pc => new ObterCategoriaResponse {
                    CategoriaId = pc.Categoria.CategoriaId,
                    Nome = pc.Categoria.Nome
                }).ToList(),
                Tags = produto.ProdutoTag.Select(pt => new ObterTagResponse {
                    TagId = pt.Tag.TagId,
                    Nome = pt.Tag.Nome
                }).ToList()
            });
        }

    }

}

