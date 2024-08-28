using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;


namespace Ingenico.Barcode.Shared.Requests {
    public class AtualizarCategoriaRequest : IRequest<Result<AtualizarCategoriaResponse>> {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = default!;
    }
}
