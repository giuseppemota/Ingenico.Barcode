using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Domain.Entites;

public class TagEntity
{
    public Guid TagId { get; set; }
    public string Nome { get; set; } = default!;

    public ICollection<ProdutoTag> ProdutoTag { get; set; } = new List<ProdutoTag>();
}