namespace RaizesDoNordeste.Application.DTOs.Responses.Usuario;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
