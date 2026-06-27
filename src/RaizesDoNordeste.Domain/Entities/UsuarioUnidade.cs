namespace RaizesDoNordeste.Domain.Entities;

public class UsuarioUnidade
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public Guid UnidadeId { get; private set; }
    public Unidade Unidade { get; private set; } = null!;

    public UsuarioUnidade(Guid usuarioId, Guid unidadeId)
    {
        UsuarioId = usuarioId;
        UnidadeId = unidadeId;
    }

    protected UsuarioUnidade() { }
}
