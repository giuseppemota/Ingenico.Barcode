using Ingenico.Barcode.Shared.Requests;


namespace Ingenico.Barcode.Domain.Entites {
    public class CategoriaEntity {
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        public Guid IdProduto { get; set; }

        // Relacionamento com Produto
        public ProdutoEntity Produto { get; set; } = default!;

        public void Atualizar(AtualizarCategoriaRequest request) {
            Nome = request.Nome;

        }
    }
}
