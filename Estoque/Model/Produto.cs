﻿namespace Estoque;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public string fornecedor{get;set;}
}
