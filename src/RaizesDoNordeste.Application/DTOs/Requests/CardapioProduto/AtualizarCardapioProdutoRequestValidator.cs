using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;

public class AtualizarCardapioProdutoRequestValidator : AbstractValidator<AtualizarCardapioProdutoRequest>
{
    public AtualizarCardapioProdutoRequestValidator()
    {
        RuleFor(x => x.PrecoVenda)
            .GreaterThan(0).WithMessage("Preço de venda deve ser maior que zero.");
    }
}
