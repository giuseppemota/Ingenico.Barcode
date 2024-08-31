namespace Ingenico.Barcode.Shared.Responses {
    public class CadastrarProdutoResponse {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public string UnidadeMedida { get; set; }
        public string Ingredientes { get; set; }
        public string PaisOrigem { get; set; }

        public List<CadastrarCategoriaResponse> Categorias { get; set; } = new List<CadastrarCategoriaResponse>();
        public List<CadastrarTagResponse> Tags { get; set; } = new List<CadastrarTagResponse>();
    }
}
