using Ingenico.Barcode.Shared.Exceptions;

namespace Ingenico.Barcode.Shared.Enums;

public class AnaliseMensagemErro
{
    public static readonly ErrorResult Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETypeError.Erro
    };

    public static readonly ErrorResult SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETypeError.Alerta
    };

    public static ErrorResult ErroGravacaoUsuario = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETypeError.Erro
    };

    public static ErrorResult DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETypeError.Erro
    };
}
