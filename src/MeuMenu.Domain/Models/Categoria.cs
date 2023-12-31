﻿namespace MeuMenu.Domain.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string CategoriaDescricao { get; set; }
    public string CategoriaImagem { get; set; }

    /* EF Relations */
    public IEnumerable<Produto> Produtos { get; set; }
}