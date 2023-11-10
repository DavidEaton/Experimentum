using Experimentum.Api.Features.Persons;
using Experimentum.Domain.Features;
using Microsoft.EntityFrameworkCore;
using Entity = Experimentum.Domain.Abstractions.Entity;

namespace Experimentum.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>().HasKey(e => e.Id);
            modelBuilder.Entity<Entity>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Ignore<Entity>();

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }

        public void EnsureSeedData()
        {
            Database.Migrate();
            SeedData.Initialize(this);
        }

        public DbSet<Person> Persons { get; set; }

    }
}
