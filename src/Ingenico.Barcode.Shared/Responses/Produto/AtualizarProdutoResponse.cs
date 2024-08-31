using System;
using System.Collections.Generic;

namespace Ingenico.Barcode.Shared.Responses
{
    public class AtualizarProdutoResponse
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public decimal Peso { get; set; }
        public string UnidadeMedida { get; set; }
        public string Ingredientes { get; set; }
        public string PaisOrigem { get; set; }
        public DateTime Validade { get; set; }
        public List<ObterCategoriaResponse> Categorias { get; set; }
        public List<ObterTagResponse> Tags { get; set; }
    }
}
