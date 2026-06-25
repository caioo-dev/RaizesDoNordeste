namespace RaizesDoNordeste.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime DataNascimento { get; set; }
    public DateTime DataInclusao { get; set; } = DateTime.Now;

    public Cliente(string nome, string email, DateTime dataNascimento, string telefone, string observacao, string documento)
    {
        Nome = nome;
        Email = email; 
        Telefone = telefone;
        Observacao = observacao;
        Documento = documento;
        DataNascimento = dataNascimento;
    }
    
    private Cliente() { }

    public void Atualizar(
        string nome,
        string email,
        string telefone,
        string documento,
        string observacao,
        bool ativo,
        DateTime dataNascimento)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Documento = documento;
        Observacao = observacao;
        Ativo = ativo;
        DataNascimento = dataNascimento;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}
