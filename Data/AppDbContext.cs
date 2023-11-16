using BancoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<Conta> Contas { get; set; }
        public DbSet<Extrato> Extratos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db; Cache=Shared");
        }
    }
}
