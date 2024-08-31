using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ExcluirProdutoRequest : IRequest<Result<ExcluirProdutoResponse>> {
        public Guid ProdutoId { get; set; }
    }
}
    