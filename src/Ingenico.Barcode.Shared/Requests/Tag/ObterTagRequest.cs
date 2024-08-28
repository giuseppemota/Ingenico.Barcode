

using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ObterTagRequest :  IRequest<Result<ObterTagResponse>> {
        public int TagId { get; set; }
    }
}
