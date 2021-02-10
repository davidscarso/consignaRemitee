using Microsoft.EntityFrameworkCore;

namespace CotizacionesAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename=./QuotesDB.sqlite");
        }

        public DbSet<QuoteModel> Quotes { get; set; }

    }
}