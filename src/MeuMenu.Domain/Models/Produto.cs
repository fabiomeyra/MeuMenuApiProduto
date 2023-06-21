namespace MeuMenu.Domain.Models;

public class Produto
{
    public Guid ProdutoId { get; set; }
    public string ProdutoDescricao { get; set; }
    public bool ProdutoAtivo { get; set; }
    public string ProdutoImagem { get; set; }
    public decimal ProdutoValor { get; set; }
    public string ProdutoIngredientes { get; set; }
    public int? ProdutoCalorias { get; set; }
    public string? ProdutoAlergias { get; set; }
    public int CategoriaId { get; set; }

    /* EF Relation */
    public Categoria Categoria { get; set; }

    public void GerarId()
    {
        ProdutoId = Guid.NewGuid();
    }
}