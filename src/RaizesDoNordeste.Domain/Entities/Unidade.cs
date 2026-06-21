namespace RaizesDoNordeste.Domain.Entities;

public class Unidade
{
    public Guid Id { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Ie { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime? DataExclusao { get; set; }

    public Unidade(
        string nomeFantasia,
        string razaoSocial,
        string cnpj,
        string ie
        )
    {
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        Ie = ie;
        Ativo = true;
    }

    private Unidade() { }

    public ICollection<UsuarioUnidade> UsuarioUnidades { get; } = [];
    public ICollection<ProdutoUnidade> ProdutoUnidades { get; } = [];

    public void Atualizar(string nomeFantasia, string razaoSocial, string cnpj, string ie, bool ativo)
    {
        NomeFantasia = nomeFantasia;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
        Ie = ie;
        Ativo = ativo;
    }

    public void Excluir()
    {
        Ativo = false;
        DataExclusao = DateTime.UtcNow;
    }
}
