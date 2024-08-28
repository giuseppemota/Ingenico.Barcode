using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;


namespace Ingenico.Barcode.Shared.Requests {
    public class AtualizarTagRequest : IRequest<Result<AtualizarTagResponse>> {
        public Guid IdTag { get; set; }
        public string NomeTag { get; set; } = default!;
    }
}
