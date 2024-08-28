namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarCategoriaResponse {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        public int IdProduto { get; set; }
    }
}
