using Microsoft.AspNetCore.Http;

namespace MeuMenu.Application.ViewModels;


public class ProdutoAddViewModel
{
    public Guid? ProdutoId { get; set; }
    public string? ProdutoDescricao { get; set; }
    public bool ProdutoAtivo { get; set; }
    public decimal ProdutoValor { get; set; }
    public string? ProdutoIngredientes { get; set; }
    public int ProdutoCalorias { get; set; }
    public string? ProdutoAlergias { get; set; }
    public int CategoriaId { get; set; }
    public string? ProdutoImagem { get; set; }

}