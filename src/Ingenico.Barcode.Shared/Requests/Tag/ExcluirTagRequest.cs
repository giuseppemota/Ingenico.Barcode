using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;


namespace Ingenico.Barcode.Shared.Requests{
    public class ExcluirTagRequest : IRequest<Result<ExcluirTagResponse>> {
        public Guid TagId { get; set; }
    }
}
