using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class Tag {
        public int TagId { get; set; }
        public string NomeTag { get; set; }

        // Relacionamento com Produto (Muitos para Muitos)
        public ICollection<Produto> Produtos { get; set; } 
    }
}
