using FluentValidation;

namespace RaizesDoNordeste.Application.DTOs.Requests.Pedido;

public class CriarPedidoRequestValidator : AbstractValidator<CriarPedidoRequest>
{
    public CriarPedidoRequestValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty().WithMessage("ClienteId é obrigatório.");

        RuleFor(x => x.UnidadeId)
            .NotEmpty().WithMessage("UnidadeId é obrigatório.");

        RuleFor(x => x.CardapioId)
            .NotEmpty().WithMessage("CardapioId é obrigatório.");

        RuleFor(x => x.EnderecoEntrega)
            .NotNull().WithMessage("Endereço de entrega é obrigatório.");

        RuleFor(x => x.EnderecoEntrega.Logradouro)
            .NotEmpty().WithMessage("Logradouro é obrigatório.")
            .MaximumLength(300).WithMessage("Logradouro deve ter no máximo 300 caracteres.");

        RuleFor(x => x.EnderecoEntrega.Numero)
            .NotEmpty().WithMessage("Número é obrigatório.")
            .MaximumLength(50).WithMessage("Número deve ter no máximo 50 caracteres.");

        RuleFor(x => x.EnderecoEntrega.Bairro)
            .NotEmpty().WithMessage("Bairro é obrigatório.")
            .MaximumLength(150).WithMessage("Bairro deve ter no máximo 150 caracteres.");

        RuleFor(x => x.EnderecoEntrega.Cep)
            .NotEmpty().WithMessage("CEP é obrigatório.")
            .MaximumLength(20).WithMessage("CEP deve ter no máximo 20 caracteres.");

        RuleFor(x => x.Produtos)
            .NotEmpty().WithMessage("O pedido deve ter pelo menos um produto.");

        RuleForEach(x => x.Produtos).ChildRules(produto =>
        {
            produto.RuleFor(p => p.ProdutoId)
                .NotEmpty().WithMessage("ProdutoId é obrigatório.");

            produto.RuleFor(p => p.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");
        });
    }
}
