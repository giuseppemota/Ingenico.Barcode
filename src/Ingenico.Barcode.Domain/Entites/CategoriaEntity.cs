using Ingenico.Barcode.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites {
    public class CategoriaEntity {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = default!;

        public int IdProduto { get; set; }

        // Relacionamento com Produto
        public ProdutoEntity Produto { get; set; } = default!;

        public void Atualizar(AtualizarCategoriaRequest request) {
            Nome = request.Nome;

        }
    }
}
