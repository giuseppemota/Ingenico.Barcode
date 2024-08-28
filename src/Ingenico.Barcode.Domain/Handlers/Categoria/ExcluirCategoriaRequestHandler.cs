using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;


namespace Ingenico.Barcode.Domain.Handlers {
    public class ExcluirCategoriaRequestHandler : IRequestHandler<ExcluirCategoriaRequest, Result<ExcluirCategoriaResponse>> {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExcluirCategoriaRequestHandler> _logger;
        public ExcluirCategoriaRequestHandler(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, ILogger<ExcluirCategoriaRequestHandler> logger) {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<ExcluirCategoriaResponse>> Handle(ExcluirCategoriaRequest request, CancellationToken cancellationToken) {
            var categoria = await _categoriaRepository.ObterCategoriaAsync(request.CategoriaId);

            if (categoria is null) {
                _logger.LogError("Endereço não encontrado");
                //return Result.Error<ExcluirEnderecoResponse>(new Compartilhado.Exceptions.EnderecoNaoEncontradoExcecao());
            }

            await _categoriaRepository.ExcluirCategoriaAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Endereço excluido");

            return Result.Success(new ExcluirCategoriaResponse());
        }
    }
}
