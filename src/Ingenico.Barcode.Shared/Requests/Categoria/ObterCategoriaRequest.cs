using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ObterCategoriaRequest : IRequest<Result<ObterCategoriaResponse>> { 
        public int CategoriaId { get; set; }
    }
}
