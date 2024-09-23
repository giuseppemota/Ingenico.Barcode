

using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class CadastrarTagRequest : IRequest<Result<CadastrarTagResponse>> {
        public string Nome { get; set; } = default!;
    }
}
