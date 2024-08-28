namespace Ingenico.Barcode.Shared.Responses {
    public class ObterProdutoResponse {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public DateTime Validade { get; set; }
        public double Peso { get; set; }
        public double Preco { get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!;

        public ObterCategoriaResponse? Categoria { get; set; }
        public ObterTagResponse? Tag { get; set; }
    }
}
