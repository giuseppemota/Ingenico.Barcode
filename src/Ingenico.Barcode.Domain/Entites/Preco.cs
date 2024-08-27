using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class Preco {
        public int PrecoId { get; set; }
        public decimal PrecoOriginal { get; set; }
        public decimal PrecoDesconto { get; set; }
        public DateTime DataUpdate { get; set; }

        // Relacionamento com Produto
        public int ProdutoId { get; set; } 
        public Produto Produto { get; set; } 
    }
}
