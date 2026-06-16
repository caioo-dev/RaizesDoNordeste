namespace RaizesDoNordeste.Domain.Entities;

public class UsuarioUnidade
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public Guid UnidadeId { get; set; }
    public Unidade Unidade { get; set; } = null!;
}
