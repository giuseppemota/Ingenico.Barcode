using Ingenico.Barcode.Shared.Enums;

namespace Ingenico.Barcode.Shared.Exceptions;

public class ErrorResult
{
    public string Titulo { get; set; } = default!;

    public string Descricao { get; set; } = default!;

    public ETypeError Tipo { get; set; }
}
