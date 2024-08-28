using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers{
    public class ExcluirProdutoRequestHandler : IRequestHandler<ExcluirProdutoRequest, Result<ExcluirProdutoResponse>> {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExcluirProdutoRequestHandler> _logger;

        public ExcluirProdutoRequestHandler(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, ILogger<ExcluirProdutoRequestHandler> logger) {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<ExcluirProdutoResponse>> Handle(ExcluirProdutoRequest request, CancellationToken cancellationToken) {
            var produto = await _produtoRepository.ObterProdutoAsync(request.ProdutoId);

            if (produto is null) {
                _logger.LogError("Produto não encontrada.");
                //return Result.Error<ExcluirPessoaResponse>(new Compartilhado.Exceptions.PessoaNaoEncontradaExcecao());
            }

            await _produtoRepository.ExcluirProdutoAsync(produto);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Produto excluido.");

            return Result.Success(new ExcluirProdutoResponse());
        }

    }
}
