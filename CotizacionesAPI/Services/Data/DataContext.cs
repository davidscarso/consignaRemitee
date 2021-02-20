using CotizacionesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CotizacionesAPI.Services.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename=./Services/Data/QuotesDB.sqlite");
        }

        public DbSet<QuoteModel> Quotes { get; set; }

    }
}