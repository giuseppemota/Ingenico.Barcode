
namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarTagResponse {
        public int TagId { get; set; }
        public string NomeTag { get; set; } = default!;

        public int IdProduto { get; set; }
    }
}
