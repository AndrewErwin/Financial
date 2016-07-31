using System.Configuration;
using Financial.Models.Entities;
using Microsoft.Data.Entity;

namespace Financial.Models.Context
{
    public class FinancialContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCardNetwork> CreditCardNetworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Financial"].ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}