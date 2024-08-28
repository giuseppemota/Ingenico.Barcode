using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class TagEntity {
        public int TagId { get; set; }
        public string NomeTag { get; set; } = default!;

        public int IdProduto {  get; set; } 

        // Relacionamento com Produto (Muitos para Muitos)
        public ProdutoEntity Produto { get; set; } = default!;
    }
}
