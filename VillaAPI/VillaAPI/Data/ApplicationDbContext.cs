using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Villa>().HasData(
            //    new Villa()
            //    {
            //        Id = 1,
            //        Address = "Abc",
            //        Name = "Name1",
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //    }

            //    ); 

        }
        public DbSet<Villa> Villas { get; set; }

    }
}
