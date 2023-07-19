using Microsoft.EntityFrameworkCore;
using MyBGList.Models;

namespace MyBGList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BoardGamesDomains>()
                .HasKey((item) => new { item.BoardGameId, item.DomainId });


            modelBuilder.Entity<BoardGamesMechanics>()
                .HasKey(item => new { item.BoardGameId, item.MechanicId });


        }


    }
}
