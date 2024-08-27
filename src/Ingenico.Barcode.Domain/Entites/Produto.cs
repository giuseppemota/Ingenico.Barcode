using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class Produto {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public DateTime Validade { get; set; }
        public double Peso { get; set; }
        public string UnidadeMedida { get; set; } = default!;
        public string Ingredientes { get; set; } = default!;
        public string PaisOrigem { get; set; } = default!; 

        // Relacionamento com Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } 

        // Relacionamento com QRCode
        public int QRCodeId { get; set; }
        public QRCode QRCode { get; set; }

        // Relacionamento com Tags (Muitos para Muitos)
        public ICollection<Tag> Tags { get; set; }

        // Relacionamento com Preço
        public ICollection<Preco> Precos { get; set; }
    }
}
