namespace RaizesDoNordeste.Application.DTOs.Responses.Cliente;

public class ClienteObterTodosResponse
{
    public IEnumerable<ClienteObterTodosModel> Clientes { get; set; } = [];
}

public class ClienteObterTodosModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
