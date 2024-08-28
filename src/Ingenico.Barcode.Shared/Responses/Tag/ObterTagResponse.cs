namespace Ingenico.Barcode.Shared.Responses {
    public class ObterTagResponse {
        public int TagId { get; set; }
        public string NomeTag { get; set; } = default!;

        public int IdProduto { get; set; }
    }
}
