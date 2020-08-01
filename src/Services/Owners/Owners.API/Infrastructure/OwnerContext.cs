using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Owners.API.EntityConfigurations;
using Owners.API.Model;

namespace Owners.API.Infrastructure
{
    public class OwnerContext: DbContext
    {
        public OwnerContext(DbContextOptions<OwnerContext> options) : base(options)
        {
        }
        public DbSet<Owner> Owners { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OwnersEntityTypeConfiguration());
        }
    }

    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<OwnerContext>
    {
        //called at migration only for design tools
        public OwnerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OwnerContext>()
                .UseSqlServer("Server=.;Initial Catalog=Broker.Service.OwnersDb;Integrated Security=true");

            return new OwnerContext(optionsBuilder.Options);
        }

    }
}
