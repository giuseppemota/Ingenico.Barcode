namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarProdutoResponse {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!;
        public string ImagePath { get; set; } = default!;

        // Categorias associadas ao produto
        public List<CadastrarCategoriaResponse> Categorias { get; set; } = new List<CadastrarCategoriaResponse>();

        // Tags associadas ao produto
        public List<CadastrarTagResponse> Tags { get; set; } = new List<CadastrarTagResponse>();

        // Imagens associadas ao produto
        public List<CadastrarImagemResponse> Imagens { get; set; } = new List<CadastrarImagemResponse>();
    }

}
