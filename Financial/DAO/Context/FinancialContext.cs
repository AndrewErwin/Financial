using System.Configuration;
using Financial.Models.Entities;
using Microsoft.Data.Entity;

namespace Financial.Models.Context
{
    public class FinancialContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCardNetwork> CreditCardNetworks { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Financial"].ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}