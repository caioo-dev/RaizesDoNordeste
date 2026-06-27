using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.ReservaEstoque;

public class CriarReservaEstoqueRequestValidator : AbstractValidator<CriarReservaEstoqueRequest>
{
    public CriarReservaEstoqueRequestValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("ProdutoId é obrigatório.");

        RuleFor(x => x.UnidadeId)
            .NotEmpty().WithMessage("UnidadeId é obrigatório.");

        RuleFor(x => x.DocumentoOrigemId)
            .NotEmpty().WithMessage("DocumentoOrigemId é obrigatório.");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");

        RuleFor(x => x.DataExpiracao)
            .GreaterThan(DateTime.UtcNow).WithMessage("Data de expiração deve ser futura.");

        RuleFor(x => x.TipoMovimentacaoOrigem)
            .IsInEnum().WithMessage("Tipo de movimentação inválido.");
    }
}
