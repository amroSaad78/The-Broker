using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    public class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.Property(ci => ci.Id)
                .UseHiLo("sale_hilo")
                .IsRequired();

            builder.HasOne(ci => ci.Apartment)
                .WithMany()
                .HasForeignKey(ci => ci.ApartmentId);
        }
    }
}
