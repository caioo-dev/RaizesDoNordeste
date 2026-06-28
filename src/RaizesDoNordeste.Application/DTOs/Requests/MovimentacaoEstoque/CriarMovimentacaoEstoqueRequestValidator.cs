using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.MovimentacaoEstoque;

public class CriarMovimentacaoEstoqueRequestValidator : AbstractValidator<CriarMovimentacaoEstoqueRequest>
{
    public CriarMovimentacaoEstoqueRequestValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("ProdutoId é obrigatório.");

        RuleFor(x => x.UnidadeId)
            .NotEmpty().WithMessage("UnidadeId é obrigatório.");

        RuleFor(x => x.DocumentoOrigemId)
            .NotEmpty().WithMessage("DocumentoOrigemId é obrigatório.");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");

        RuleFor(x => x.TipoMovimentacaoOrigem)
            .IsInEnum().WithMessage("Tipo de movimentação inválido.");

        RuleFor(x => x.Observacao)
            .MaximumLength(1000).WithMessage("Observação deve ter no máximo 1000 caracteres.");
    }
}
