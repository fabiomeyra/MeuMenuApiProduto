using FluentValidation;
using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Validations;

public class ExcluirProdutoValidation : AbstractValidator<Produto>
{

    public ExcluirProdutoValidation()
    {
        RuleFor(x => x.ProdutoId)
            .Must(x => x != Guid.Empty)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DeveInformarIdentificadorExluir));
    }
    
    private string RetornaMensagemFormatado(string mensage) => $"(Produto): {mensage}";
}