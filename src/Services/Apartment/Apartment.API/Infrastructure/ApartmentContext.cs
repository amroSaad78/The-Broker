using Apartment.API.EntityConfigurations;
using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Apartment.API.Infrastructure
{
    public class ApartmentContext: DbContext
    {
        public ApartmentContext(DbContextOptions<ApartmentContext> options) : base(options)
        {
        }
        public DbSet<Model.Apartment> Apartment { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Bedrooms> Bedroom { get; set; }
        public DbSet<Countries> Country { get; set; }
        public DbSet<Furnishings> Furniture { get; set; }
        public DbSet<Periods> Period { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApartmentEntityTypeConfiguration());
            builder.ApplyConfiguration(new RentEntityTypeConfiguration());
            builder.ApplyConfiguration(new SaleEntityTypeConfiguration());
            builder.ApplyConfiguration(new BedroomsEntityTypeConfiguration());
            builder.ApplyConfiguration(new CountriesEntityTypeConfiguration());
            builder.ApplyConfiguration(new FurnishingsEntityTypeConfiguration());
            builder.ApplyConfiguration(new PeriodsEntityTypeConfiguration());            
        }
    }


    public class ApartmentContextDesignFactory : IDesignTimeDbContextFactory<ApartmentContext>
    {
        //called at migration only for design tools
        public ApartmentContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApartmentContext>()
                .UseSqlServer("Server=tcp:192.168.100.110:1433;Database=Broker.Service.ApartmentDb;User Id=sa;Password=*txKM732@z58G");

            return new ApartmentContext(optionsBuilder.Options);
        }

    }

}