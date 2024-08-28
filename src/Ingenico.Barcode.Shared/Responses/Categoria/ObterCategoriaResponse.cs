namespace Ingenico.Barcode.Shared.Responses {
    public class ObterCategoriaResponse {
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        public Guid IdProduto { get; set; }
    }
}
