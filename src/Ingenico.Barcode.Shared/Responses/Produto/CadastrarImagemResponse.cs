namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarImagemResponse {
        public Guid ImagemId { get; set; }
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
}
