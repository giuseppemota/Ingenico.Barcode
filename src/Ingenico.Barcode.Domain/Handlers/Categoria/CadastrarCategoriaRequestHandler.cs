using Ingenico.Barcode.Domain.Entites;
using Ingenico.Barcode.Domain.Repository;
using Ingenico.Barcode.Shared.Requests;
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;


namespace Ingenico.Barcode.Domain.Handlers {
    public class CadastrarCategoriaRequestHandler : IRequestHandler<CadastrarCategoriaRequest, Result<CadastrarCategoriaResponse>> {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CadastrarCategoriaRequestHandler> _logger;

        public CadastrarCategoriaRequestHandler(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository, IUnitOfWork unitOfWork, ILogger<CadastrarCategoriaRequestHandler> logger) {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<CadastrarCategoriaResponse>> Handle(CadastrarCategoriaRequest request, CancellationToken cancellationToken) {
         

            var categoria = new CategoriaEntity() {
               
                Nome = request.Nome,
                IdProduto = request.IdProduto

            };

            await _categoriaRepository.CadastrarCategoriaAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Categoria cadastrada");

            return Result.Success(new CadastrarCategoriaResponse() {

                CategoriaId = categoria.CategoriaId,
                Nome = request.Nome,
                IdProduto = request.IdProduto


            });
        }
    }
}
