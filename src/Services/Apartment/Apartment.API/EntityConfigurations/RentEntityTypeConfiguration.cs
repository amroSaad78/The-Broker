using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    public class RentEntityTypeConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.ToTable("Rent");

            builder.Property(ci => ci.Id)
                .UseHiLo("rent_hilo")
                .IsRequired();

            builder.HasOne(ci => ci.Apartment)
                .WithMany()
                .HasForeignKey(ci => ci.ApartmentId);

            builder.HasOne(ci => ci.Period)
                .WithMany()
                .HasForeignKey(ci => ci.PeriodId);
        }
    }
}
