using Ingenico.Barcode.Shared.Requests;


namespace Ingenico.Barcode.Domain.Entites;

public class CategoriaEntity
{
    public Guid CategoriaId { get; set; }
    public string Nome { get; set; } = default!;

    public ICollection<ProdutoCategoria> ProdutoCategoria { get; set; } = new List<ProdutoCategoria>();
}
