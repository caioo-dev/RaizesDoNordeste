namespace RaizesDoNordeste.Domain.Entities;

public class Unidade
{
    public Guid Id { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string IE { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime? DataExclusao { get; set; }

    public ICollection<UsuarioUnidade> UsuarioUnidades { get; } = [];
    public ICollection<ProdutoUnidade> ProdutoUnidades { get; } = [];
}
