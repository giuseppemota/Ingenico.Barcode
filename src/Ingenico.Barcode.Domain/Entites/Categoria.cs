using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class Categoria {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        // Relacionamento com Produto
        public ICollection<Produto> Produtos { get; set; }
    }
}
