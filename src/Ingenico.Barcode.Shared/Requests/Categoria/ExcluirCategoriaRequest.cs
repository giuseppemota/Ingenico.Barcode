using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class ExcluirCategoriaRequest : IRequest<Result<ExcluirCategoriaResponse>> {
        public int CategoriaId { get; set; }
    }
}
