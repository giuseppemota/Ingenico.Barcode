using Ingenico.Barcode.Shared.Exceptions;

namespace Ingenico.Barcode.Shared.Enums;

public class AuthError
{
    public static readonly ErrorResult UsuarioNaoEncontrado = new()
    {
        Titulo = "Usuário ou senha incorretos",
        Descricao = "Os dados do usuário não foram encontrados",
        Tipo = ETypeError.Erro
    };
}