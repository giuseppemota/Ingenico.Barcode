using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Enums;
using Ingenico.Barcode.Shared.Exceptions;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Handlers;

public class ExcluirProdutoRequestHandler : IRequestHandler<ExcluirProdutoRequest, Result<ExcluirProdutoResponse>>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ExcluirProdutoRequestHandler> _logger;

    public ExcluirProdutoRequestHandler(
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork,
        ILogger<ExcluirProdutoRequestHandler> logger)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<ExcluirProdutoResponse>> Handle(ExcluirProdutoRequest request, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);
        if (produto == null)
        {
            _logger.LogWarning("Produto não encontrado: {ProdutoId}", request.ProdutoId);
            return Result.Error<ExcluirProdutoResponse>(new ExceptionAplication(AuthError.UsuarioNaoEncontrado));
        }

        await _produtoRepository.ExcluirProdutoAsync(produto);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Produto excluído: {ProdutoId}", request.ProdutoId);
        return Result.Success(new ExcluirProdutoResponse());
    }
}
