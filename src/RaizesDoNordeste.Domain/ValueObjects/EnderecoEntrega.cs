namespace RaizesDoNordeste.Domain.ValueObjects;

public class EnderecoEntrega
{
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;

    public EnderecoEntrega(string logradouro, string numero, string bairro, string cep)
    {
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cep = cep;
    }
    protected EnderecoEntrega() { }
}
