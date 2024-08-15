namespace Ingenico.Barcode.Shared.Exceptions;

public class ExceptionAplication : Exception
{
    public ExceptionAplication(ErrorResult erro)
     : base(erro.Descricao) => ResponseErro = erro;

    public ErrorResult ResponseErro { get; set; }
}
