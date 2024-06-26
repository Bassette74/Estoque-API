using Estoque;

public class EstoqueService
{
    private readonly EstoqueDbContext _context;

    public EstoqueService(EstoqueDbContext context)
    {
        _context = context;
    }

    public async Task MoverProdutoParaEstoqueAsync(int produtoId, int quantidade)
    {
        var produto = await _context.Produtos.FindAsync(produtoId);
        if (produto == null)
        {
            throw new Exception($"Produto com ID {produtoId} não encontrado.");
        }

        var estoque = new Estoques
        {
            ProdutoId = produtoId,
            Quantidade = quantidade,
            DataMovimentacao = DateTime.Now
        };

        _context.Estoquess.Add(estoque);
        await _context.SaveChangesAsync();
    }
}
