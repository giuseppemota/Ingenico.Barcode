namespace Ingenico.Barcode.Shared.Responses{
    public class AtualizarTagResponse {
        public Guid TagId { get; set; }
        public string NomeTag { get; set; } = default!;
    }
}
