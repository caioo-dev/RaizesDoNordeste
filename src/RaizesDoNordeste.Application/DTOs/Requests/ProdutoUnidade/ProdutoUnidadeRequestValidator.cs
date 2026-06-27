using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.ProdutoUnidade;
public class ProdutoUnidadeRequestValidator : AbstractValidator<ProdutoUnidadeRequest>
{
    public ProdutoUnidadeRequestValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("ProdutoId é obrigatório.");

        RuleFor(x => x.EstoqueDisponivel)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque disponível não pode ser negativo.");

        RuleFor(x => x.EstoqueReservado)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque reservado não pode ser negativo.");   
    }
}
