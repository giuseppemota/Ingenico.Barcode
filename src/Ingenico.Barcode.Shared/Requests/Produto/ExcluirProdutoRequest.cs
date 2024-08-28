using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ExcluirProdutoRequest : IRequest<Result<ExcluirProdutoResponse>> {
        public int ProdutoId { get; set; }
    }
}
