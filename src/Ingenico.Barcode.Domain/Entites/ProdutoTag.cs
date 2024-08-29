using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites;

public class ProdutoTag
{
    public Guid ProdutoId { get; set; }
    public ProdutoEntity Produto { get; set; } = default!;

    public Guid TagId { get; set; }
    public TagEntity Tag { get; set; } = default!;
}