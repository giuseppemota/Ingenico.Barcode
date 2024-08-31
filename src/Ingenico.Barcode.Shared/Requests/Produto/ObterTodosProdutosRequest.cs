using MediatR;
using OperationResult;
using Ingenico.Barcode.Shared.Responses;

namespace Ingenico.Barcode.Shared.Requests {
    public class ObterTodosProdutosRequest : IRequest<Result<ObterTodosProdutosResponse>> {

    }
}
