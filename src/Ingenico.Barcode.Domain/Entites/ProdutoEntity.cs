using Ingenico.Barcode.Shared.Requests;

namespace Ingenico.Barcode.Domain.Entites {
    public class ProdutoEntity {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public DateTime Validade { get; set; }
        public double Peso { get; set; }
        public double Preco {  get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!;


        // Relacionamento com Categoria
        //public int CategoriaId { get; set; }
        //public CategoriaEntity Categoria { get; set; }
        public ICollection<CategoriaEntity> Categorias { get; set; } = new List<CategoriaEntity>();
        // Relacionamento com Tags (Muitos para Muitos)
        public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();

        // Relacionamento com Preço
        //public ICollection<PrecoEntity> Precos { get; set; } = new List<PrecoEntity>();

        public void Atualizar(AtualizarProdutoRequest request) {
            Nome = request.Nome;
            Descricao = request.Descricao;
            Marca = request.Marca;
            Validade = request.Validade;
            Peso = request.Peso;
            Preco = request.Preco;
            UnidadeMedida = request.UnidadeMedida;
            Ingredientes = request.Ingredientes;
            PaisOrigem = request.PaisOrigem;

        }
    }
}
