namespace RaizesDoNordeste.Application.DTOs.Requests.Unidade;

public class AtualizarUnidadeRequest
{
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string IE { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
