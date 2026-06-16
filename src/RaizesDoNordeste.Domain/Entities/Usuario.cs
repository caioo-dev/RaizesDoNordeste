namespace RaizesDoNordeste.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public DateTime DataInclusao { get; set; } = DateTime.UtcNow;
    public bool Ativo { get; set; } = true;

    public ICollection<UsuarioUnidade> UsuarioUnidades { get; } = [];
}
