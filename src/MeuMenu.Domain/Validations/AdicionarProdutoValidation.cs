using FluentValidation;
using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models;

namespace MeuMenu.Domain.Validations;

public class AdicionarProdutoValidation : AbstractValidator<Produto>
{
    private readonly IProdutoService _produtoService;

    public AdicionarProdutoValidation(IProdutoService produtoService)
    {
        _produtoService = produtoService;
        ValidarCamposObrigatorios();
    }
    
    private void ValidarCamposObrigatorios()
    {
        RuleFor(x => x.ProdutoDescricao)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DescricaoObrigatorio));

        RuleFor(x => x.ProdutoAtivo)
            .NotEmpty()
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.AtivoObrigatorio));

        RuleFor(x => x.ProdutoImagem)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.ImagemObrigatoria));

        RuleFor(x => x.ProdutoValor)
            .GreaterThan(0)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.ValorObrigatorio));

        RuleFor(x => x.ProdutoIngredientes)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.IngredientesObrigatorio));

        RuleFor(x => x.ProdutoCalorias)
            .GreaterThan(0)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.CaloriasObrigatorio));

        RuleFor(x => x.ProdutoAlergias)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.AlergiasObrigatorio));

        RuleFor(x => x.CategoriaId)
            .Must(x => x > 0 && Enum.IsDefined(typeof(CategoriaEnum), x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.CategoriaObrigatorio));
    }

    private bool CamposObrigatoriosPreenchidos(Produto produto)
    {
        return
            !string.IsNullOrWhiteSpace(produto.ProdutoImagem)
            && !string.IsNullOrWhiteSpace(produto.ProdutoAlergias)
            && !string.IsNullOrWhiteSpace(produto.ProdutoDescricao)
            && !string.IsNullOrWhiteSpace(produto.ProdutoIngredientes)
            && produto.ProdutoId != Guid.Empty
            && produto.ProdutoValor > 0
            && Enum.IsDefined(typeof(CategoriaEnum), produto.CategoriaId);
    }

    private string RetornaMensagemFormatado(string mensage) => $"(Produto): {mensage}";
}