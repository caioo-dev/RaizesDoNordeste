using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.Cardapio;

public class CriarCardapioRequestValidator : AbstractValidator<CardapioRequest>
{
    public CriarCardapioRequestValidator()
    {
        RuleFor(x => x.UnidadeId)
            .NotEmpty().WithMessage("UnidadeId é obrigatório.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");
    }
}
