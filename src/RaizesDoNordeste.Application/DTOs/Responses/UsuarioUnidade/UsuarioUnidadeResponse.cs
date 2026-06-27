namespace RaizesDoNordeste.Application.DTOs.Responses.UsuarioUnidade;

public class UsuarioUnidadeResponse
{
    public IEnumerable<UsuarioUnidadeModel> Unidades { get; set; } = [];
}

public class UsuarioUnidadeModel
{
    public Guid UnidadeId { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
