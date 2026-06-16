using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.Cliente;

public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
{
    public CriarClienteRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Name is required");
    }
}
