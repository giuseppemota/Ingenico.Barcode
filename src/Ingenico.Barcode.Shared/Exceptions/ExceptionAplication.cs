namespace Ingenico.Barcode.Shared.Exceptions;

public class ExcecaoAplicacao : Exception
{
    public ExcecaoAplicacao(ErrorResult erro)
     : base(erro.Descricao) => ResponseErro = erro;

    public ErrorResult ResponseErro { get; set; }
}
