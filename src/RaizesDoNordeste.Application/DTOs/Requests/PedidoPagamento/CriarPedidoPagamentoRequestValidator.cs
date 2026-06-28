using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.PedidoPagamento;

public class CriarPedidoPagamentoRequestValidator : AbstractValidator<CriarPedidoPagamentoRequest>
{
    public CriarPedidoPagamentoRequestValidator()
    {
        RuleFor(x => x.TipoPagamento)
            .IsInEnum().WithMessage("Tipo de pagamento inválido.")
            .NotEqual(Domain.Enums.TipoPagamento.None).WithMessage("Tipo de pagamento é obrigatório.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("Valor deve ser maior que zero.");
    }
}
