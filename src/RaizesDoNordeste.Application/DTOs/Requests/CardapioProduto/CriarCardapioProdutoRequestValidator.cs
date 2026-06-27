using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.CardapioProduto;

public class CriarCardapioProdutoRequestValidator : AbstractValidator<CriarCardapioProdutoRequest>
{
    public CriarCardapioProdutoRequestValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEmpty().WithMessage("ProdutoId é obrigatório.");

        RuleFor(x => x.PrecoVenda)
            .GreaterThan(0).WithMessage("Preço de venda deve ser maior que zero.");
    }
}
