using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.DTOs;
using Ingenico.Barcode.Shared.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers;
public class ObterProdutosSimilaresRequestHandler : IRequestHandler<ObterProdutosSimilaresRequest, Result<List<ProdutoResponseDTO>>>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ObterProdutosSimilaresRequestHandler> _logger;

    public ObterProdutosSimilaresRequestHandler(IProdutoRepository produtoRepository, ILogger<ObterProdutosSimilaresRequestHandler> logger)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
    }

    public async Task<Result<List<ProdutoResponseDTO>>> Handle(ObterProdutosSimilaresRequest request, CancellationToken cancellationToken)
    {
        // 1. Verifique se o ProdutoId foi fornecido
        if (request.ProdutoId == Guid.Empty)
        {
            //logger

            return Result.Error<List<ProdutoResponseDTO>>(new Exception("ProdutoId é obrigatório."));
        }

        // 2. Busque o produto original pelo ID
        var produtoOriginal = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);

        if (produtoOriginal == null)
        {
            return Result.Error<List<ProdutoResponseDTO>>(new Exception($"Produto com ID {request.ProdutoId} não encontrado."));
        }

        // 3. Busque as tags associadas ao produto
        var produtoTags = produtoOriginal.ProdutoTag.ToList();

        if (!produtoTags.Any())
        {
            return new List<ProdutoResponseDTO>(); // Se o produto não tiver tags, não há produtos similares		Tag	null	Ingenico.Barcode.Domain.Entites.TagEntity

        }

        // 4. Chame o método para obter os produtos com tags em comum
        var produtosSimilares = await _produtoRepository.ObterProdutosPorTagsAsync(produtoTags);

        if (produtosSimilares == null || !produtosSimilares.Any())
        {
            return new List<ProdutoResponseDTO>(); // Se não houver produtos similares, retorne uma lista vazia
        }

        var response = produtosSimilares.Select(p => new ProdutoResponseDTO
        {
            ProdutoId = p.ProdutoId,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            Marca = p.Marca,
            Peso = p.Peso,
            Validade = p.Validade,
            Lote = p.Lote,
            DataFabricacao = p.DataFabricacao,
            Ingredientes = p.Ingredientes,
            PaisOrigem = p.PaisOrigem,
            UnidadeMedida = p.UnidadeMedida

        }).ToList();

        return response;
    }
}
