using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.ClienteFidelizacao;

public class AderirProgramaFidelizacaoRequestValidator : AbstractValidator<AderirProgramaFidelizacaoRequest>
{
    public AderirProgramaFidelizacaoRequestValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("ClienteId é obrigatório.");

        RuleFor(x => x.ConsentimentoLGPD)
            .Equal(true).WithMessage("É necessário aceitar o consentimento de LGPD para aderir ao programa.");
    }
}
