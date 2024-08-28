using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;


namespace Ingenico.Barcode.Shared.Requests {
    public class CadastrarCategoriaRequest : IRequest<Result<CadastrarCategoriaResponse>> {
        public string Nome { get; set; } = default!;

        public int IdProduto { get; set; }
    }
}
