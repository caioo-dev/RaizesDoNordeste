using RaizesDoNordeste.Application.DTOs.Responses.UsuarioUnidade;
using RaizesDoNordeste.Application.Interfaces;
using RaizesDoNordeste.Domain.Entities;
using RaizesDoNordeste.Domain.Exceptions;
using RaizesDoNordeste.Domain.Interfaces;

namespace RaizesDoNordeste.Application.Services;

public class UsuarioUnidadeService(
    IUsuarioUnidadeRepository usuarioUnidadeRepository,
    IUsuarioRepository usuarioRepository,
    IUnidadeRepository unidadeRepository) : IUsuarioUnidadeService
{
    public async Task<UsuarioUnidadeResponse> ObterUnidades(Guid usuarioId, CancellationToken cancellationToken)
    {
        bool usuarioExiste = await usuarioRepository.ExistePorId(usuarioId, cancellationToken);
        if (!usuarioExiste)
        {
            throw new NotFoundException(nameof(Usuario), usuarioId);
        }
       

        IEnumerable<UsuarioUnidade> vinculos = await usuarioUnidadeRepository.ObterPorUsuario(usuarioId, cancellationToken);

        return new UsuarioUnidadeResponse
        {
            Unidades = vinculos.Select(uu => new UsuarioUnidadeModel
            {
                UnidadeId = uu.UnidadeId,
                NomeFantasia = uu.Unidade.NomeFantasia,
                RazaoSocial = uu.Unidade.RazaoSocial,
                Cnpj = uu.Unidade.Cnpj,
                Ativo = uu.Unidade.Ativo
            })
        };
    }

    public async Task Vincular(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken)
    {
        bool usuarioExiste = await usuarioRepository.ExistePorId(usuarioId, cancellationToken);
        if (!usuarioExiste)
        {
            throw new NotFoundException(nameof(Usuario), usuarioId);
        }
            

        bool unidadeExiste = await unidadeRepository.ExistePorId(unidadeId, cancellationToken);
        if (!unidadeExiste)
        {
            throw new NotFoundException(nameof(Unidade), unidadeId);
        }
            

        UsuarioUnidade? vinculoExistente = await usuarioUnidadeRepository.ObterVinculo(usuarioId, unidadeId, cancellationToken);
        if (vinculoExistente is not null)
        {
            throw new ConflictException("Usuário já está vinculado a esta unidade.");
        }
            

        var usuarioUnidade = new UsuarioUnidade(usuarioId, unidadeId);
        await usuarioUnidadeRepository.Vincular(usuarioUnidade, cancellationToken);
    }

    public async Task Desvincular(Guid usuarioId, Guid unidadeId, CancellationToken cancellationToken)
    {
        UsuarioUnidade vinculo = await usuarioUnidadeRepository.ObterVinculo(usuarioId, unidadeId, cancellationToken)
            ?? throw new NotFoundException("Vínculo entre usuário e unidade não encontrado.");

        await usuarioUnidadeRepository.Desvincular(vinculo, cancellationToken);
    }
}
