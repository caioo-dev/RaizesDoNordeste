namespace RaizesDoNordeste.Domain.Entities;

public class Cliente(string nome, string email, DateTime dataNascimento, string telefone, string observacao, string documento)
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public string Telefone { get; set; } = telefone;
    public string Documento { get; set; } = documento;
    public string Observacao { get; set; } = observacao;
    public bool Ativo { get; set; } = true;
    public DateTime DataNascimento { get; set; } = dataNascimento;
    public DateTime DataInclusao { get; set; } = DateTime.Now;
}
