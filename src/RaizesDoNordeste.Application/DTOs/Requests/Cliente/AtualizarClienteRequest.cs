namespace RaizesDoNordeste.Application.DTOs.Requests.Cliente;

public class AtualizarClienteRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
