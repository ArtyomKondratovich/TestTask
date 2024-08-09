using Microsoft.EntityFrameworkCore;
using Task1.Domain.Entities;

namespace Task1.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Rate> Rates { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>()
                .HasKey(rate => new { rate.Cur_ID, rate.Date });
        }
    }
}
