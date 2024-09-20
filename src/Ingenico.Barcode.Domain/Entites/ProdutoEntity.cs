using Ingenico.Barcode.Shared.Requests;
using static System.Net.Mime.MediaTypeNames;

namespace Ingenico.Barcode.Domain.Entites;

public class ProdutoEntity {

        public Guid ProdutoId { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public string Lote { get; set; } = default!;
        public DateTime DataFabricacao { get; set; } = default!;
        public DateTime Validade { get; set; }
        public decimal Peso { get; set; }
        public decimal Preco {  get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!;
        public string ImagePath { get; set; } = string.Empty;// Caminho da imagem no servidor
  
        public ICollection<ProdutoCategoria> ProdutoCategoria { get; set; } = new List<ProdutoCategoria>();
        public ICollection<ProdutoTag> ProdutoTag { get; set; } = new List<ProdutoTag>();
    
    public void Atualizar(AtualizarProdutoRequest request) {
            Nome = request.Nome;
            Descricao = request.Descricao;
            Marca = request.Marca;
            Lote = request.Lote;
            DataFabricacao = request.DataFabricacao;
            Validade = request.Validade;
            Peso = request.Peso;
            Preco = request.Preco;
            UnidadeMedida = request.UnidadeMedida;
            Ingredientes = request.Ingredientes;
            PaisOrigem = request.PaisOrigem;

        }
}
