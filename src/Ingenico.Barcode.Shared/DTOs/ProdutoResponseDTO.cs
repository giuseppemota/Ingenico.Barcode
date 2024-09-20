using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenico.Barcode.Shared.DTOs;

public class ProdutoResponseDTO
{
    public Guid ProdutoId { get; set; }
    public string Nome { get; set; } = default!;
    public string Descricao { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public decimal Peso { get; set; }
    public decimal Preco { get; set; }
    public string UnidadeMedida { get; set; } = default!;
    public string Ingredientes { get; set; } = default!;
    public string PaisOrigem { get; set; } = default!;
    public string Lote { get; set; } = default!;
    public DateTime DataFabricacao { get; set; }
    public DateTime Validade { get; set; }
}

