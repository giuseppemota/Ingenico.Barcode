
namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarTagResponse {
        public Guid TagId { get; set; }
        public string NomeTag { get; set; } = default!;

        public Guid IdProduto { get; set; }
    }
}
