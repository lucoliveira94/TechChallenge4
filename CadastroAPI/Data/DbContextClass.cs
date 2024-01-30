using CadastroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroAPI.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> opts) : base(opts)
        {

        }
        public DbSet<Product> Products {get;set;}
    }
}