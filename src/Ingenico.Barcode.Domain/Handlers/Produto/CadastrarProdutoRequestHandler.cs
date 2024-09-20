using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using Ingenico.Barcode.Domain.Entites;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers {
    public class CadastrarProdutoRequestHandler : IRequestHandler<CadastrarProdutoRequest, Result<CadastrarProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CadastrarProdutoRequestHandler> _logger;
        private readonly IImageUploadService _imageUploadService; // Adiciona serviço de upload de imagem

        public CadastrarProdutoRequestHandler(
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            ILogger<CadastrarProdutoRequestHandler> logger,
            IImageUploadService imageUploadService) // Injetando o serviço de upload de imagem
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageUploadService = imageUploadService;
        }

        public async Task<Result<CadastrarProdutoResponse>> Handle(CadastrarProdutoRequest request, CancellationToken cancellationToken) {
            // Faz o upload da imagem se estiver presente

            
            string imagePath = string.Empty;
            if (request.Image != null) {
                 // Realiza o upload e salva o caminho
                imagePath = _imageUploadService.UploadImage(request.Image);
            }

            var produto = new ProdutoEntity {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Marca = request.Marca,
                Validade = request.Validade,
                Preco = request.Preco,
                Peso = request.Peso,
                UnidadeMedida = request.UnidadeMedida,
                Ingredientes = request.Ingredientes,
                PaisOrigem = request.PaisOrigem,
                ImagePath = imagePath, // Armazenando o caminho da imagem
                DataFabricacao = request.DataFabricacao,
                Lote = request.Lote
            };

            // Associando categorias ao produto
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

            // Associando tags ao produto
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

            await _produtoRepository.CadastrarProdutoAsync(produto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Produto cadastrado com categorias, tags e imagem");

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
                ImagePath = produto.ImagePath, // Retorna o caminho da imagem no response
                Categorias = produto.ProdutoCategoria.Select(pc => new CadastrarCategoriaResponse {
                    CategoriaId = pc.Categoria.CategoriaId,
                    Nome = pc.Categoria.Nome
                }).ToList(),
                Tags = produto.ProdutoTag.Select(pt => new CadastrarTagResponse {
                    TagId = pt.Tag.TagId,
                    NomeTag = pt.Tag.Nome
                }).ToList()
            });
        }
    }
}
