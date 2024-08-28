
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ObterProdutoRequest : IRequest<Result<ObterProdutoResponse>> { 
        public int ProdutoId { get; set; }
    }
}
