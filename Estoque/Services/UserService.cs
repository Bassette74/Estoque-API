// Implemente a classe ProductService:
using System.Collections.Generic;
using System.Threading.Tasks;
using Estoque;
using Microsoft.EntityFrameworkCore;


namespace loja.services
{
    public class UserService
    {
        private readonly EstoqueDbContext _dbContext;

        public UserService(EstoqueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todos os produtos
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        // Métodd para consultar um produto a partir do seu Id
        public async Task<User> GetPUserByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        // Método para  gravar um novo produto
        public async Task AddUserAsync(User user)
        {
            _dbContext.Usuarios.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um produto
        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para excluir um produto
        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Usuarios.FindAsync(id);
            if (user != null)
            {
                _dbContext.Usuarios.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
