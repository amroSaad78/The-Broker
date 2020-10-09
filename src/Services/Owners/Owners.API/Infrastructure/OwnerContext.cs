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
                .UseSqlServer("Server=tcp:192.168.100.110:1433;Database=Broker.Service.OwnersDb;User Id=sa;Password=*txKM732@z58G");

            return new OwnerContext(optionsBuilder.Options);
        }

    }
}
