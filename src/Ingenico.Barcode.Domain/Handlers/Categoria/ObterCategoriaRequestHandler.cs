using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;


namespace Ingenico.Barcode.Domain.Handlers.Categoria {
    public class ObterCategoriaRequestHandler : IRequestHandler<ObterCategoriaRequest, Result<ObterCategoriaResponse>> {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<ObterCategoriaRequestHandler> _logger;
        public ObterCategoriaRequestHandler(ICategoriaRepository categoriaRepository, ILogger<ObterCategoriaRequestHandler> logger) {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }
        public async Task<Result<ObterCategoriaResponse>> Handle(ObterCategoriaRequest request, CancellationToken cancellationToken) {
            var categoria = await _categoriaRepository.ObterCategoriaAsync(request.CategoriaId);

            if (categoria is null) {
                _logger.LogError("Categoria não encontrado");
                //return Result.Error<ObterEnderecoResponse>(new Compartilhado.Exceptions.EnderecoNaoEncontradoExcecao());
            }

            _logger.LogInformation("Retornando Categoria encontrada");

            return Result.Success(new ObterCategoriaResponse() {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome
            });
        }
    }
}
