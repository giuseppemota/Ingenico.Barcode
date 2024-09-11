
using Ingenico.Barcode.Shared.Responses;
using MediatR;
using OperationResult;

namespace Ingenico.Barcode.Shared.Requests {
    public class CadastrarProdutoRequest : IRequest<Result<CadastrarProdutoResponse>> {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime Validade { get; set; }
        public string Lote { get; set; }
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public string UnidadeMedida { get; set; }
        public string Ingredientes { get; set; }
        public string PaisOrigem { get; set; }


        public List<ObterCategoriaRequest> Categorias { get; set; } = new List<ObterCategoriaRequest>();
        public List<ObterTagRequest> Tags { get; set; } = new List<ObterTagRequest>();
    }
}
