namespace Ingenico.Barcode.Shared.Responses {
    public class ObterCategoriaResponse {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        public int IdProduto { get; set; }
    }
}
