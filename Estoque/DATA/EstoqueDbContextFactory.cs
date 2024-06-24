using Estoque;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace loja.data{
public class LojaDbContextFactory : IDesignTimeDbContextFactory<EstoqueDbContext>{
        public EstoqueDbContext CreateDbContext (string [] args){

            var optonsBuilder = new DbContextOptionsBuilder<EstoqueDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

           var optionsBuilder = new DbContextOptionsBuilder<EstoqueDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)));

            return new EstoqueDbContext(optionsBuilder.Options);
        }
        
    }
}