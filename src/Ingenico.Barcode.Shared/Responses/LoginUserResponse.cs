namespace Ingenico.Barcode.Shared.Responses;

public class LoginUserResponse
{
    public LoginUserResponse(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }

    public LoginUserResponse()
    {
    }

    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}