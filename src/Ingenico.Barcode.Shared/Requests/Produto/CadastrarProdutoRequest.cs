
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class CadastrarProdutoRequest : IRequest<Result<CadastrarProdutoResponse>> {
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public DateTime Validade { get; set; }
        public double Peso { get; set; }
        public double Preco { get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!;

        public CadastrarCategoriaRequest Categoria { get; set; }
        public CadastrarTagRequest Tag { get; set; }
    }
}
