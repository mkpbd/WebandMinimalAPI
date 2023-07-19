using Microsoft.EntityFrameworkCore;
using MyBGList.Models;

namespace MyBGList.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    base.ConfigureConventions(configurationBuilder);
        //}

        public DbSet<BoardGame> BoardGames => Set<BoardGame>();
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<BoardGamesDomains> BoardGamesDomains => Set<BoardGamesDomains>();
        public DbSet<BoardGamesMechanics> BoardGamesMechanics => Set<BoardGamesMechanics>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BoardGamesDomains>()
                .HasKey((item) => new { item.BoardGameId, item.DomainId });


            modelBuilder.Entity<BoardGamesMechanics>()
                .HasKey(item => new { item.BoardGameId, item.MechanicId });

            modelBuilder.Entity<BoardGamesDomains>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGamesDomains)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGamesDomains>()
                .HasOne(o => o.Domain)
                .WithMany(m => m.BoardGamesDomains)
                .HasForeignKey(f => f.DomainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BoardGamesMechanics>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGamesMechanics)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGamesMechanics>()
                .HasOne(o => o.Mechanic)
                .WithMany(m => m.BoardGames_Mechanics)
                .HasForeignKey(f => f.MechanicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);



        }


    }
}
