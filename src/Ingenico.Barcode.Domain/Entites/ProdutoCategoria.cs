using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites;
public class ProdutoCategoria
{
    public Guid ProdutoId { get; set; }
    public ProdutoEntity Produto { get; set; } = default!;

    public Guid CategoriaId { get; set; }
    public CategoriaEntity Categoria { get; set; } = default!;
}
