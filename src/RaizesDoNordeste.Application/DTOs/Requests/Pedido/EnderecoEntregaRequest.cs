namespace RaizesDoNordeste.Application.DTOs.Requests.Pedido;

public class EnderecoEntregaRequest
{
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
}
