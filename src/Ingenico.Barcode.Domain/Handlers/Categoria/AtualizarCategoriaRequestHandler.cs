using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Ingenico.Barcode.Domain.Handlers {
    public class AtualizarCategoriaRequestHandler : IRequestHandler<AtualizarCategoriaRequest, Result<AtualizarCategoriaResponse>> {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AtualizarCategoriaRequestHandler> _logger;
        public AtualizarCategoriaRequestHandler(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, ILogger<AtualizarCategoriaRequestHandler> logger) {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<AtualizarCategoriaResponse>> Handle(AtualizarCategoriaRequest request, CancellationToken cancellationToken) {
            var categoria = await _categoriaRepository.ObterCategoriaAsync(request.CategoriaId);

            if (categoria is null) {
                _logger.LogError("Categoria não encontrado");
                //return Result.Error<AtualizarEnderecoResponse>(new Compartilhado.Exceptions.EnderecoNaoEncontradoExcecao());
            }

            categoria.Atualizar(request);
            await _categoriaRepository.AtualizarCategoriaAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Categoria atualizada");

            return Result.Success(new AtualizarCategoriaResponse() {
                CategoriaId = request.CategoriaId,
                Nome = request.Nome
            });
        }
    }
}
