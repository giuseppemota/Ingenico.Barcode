using MediatR;
using OperationResult;
using Ingenico.Barcode.Shared.Responses;

namespace Ingenico.Barcode.Shared.Requests {
    public class ObterImagemProdutoRequest : IRequest<Result<ObterImagemProdutoResponse>> {
        public Guid ProdutoId { get; set; }
    }
}

