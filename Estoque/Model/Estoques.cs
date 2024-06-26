namespace Estoque;
public class Estoques
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataMovimentacao { get; set; }
}
