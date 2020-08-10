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
                .UseHiLo("Rent_hilo")
                .IsRequired();

            builder.Property(ci => ci.OwnerId)
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.Property(ci => ci.View)
                .HasMaxLength(50);

            builder.Property(ci => ci.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ci => ci.Price)
                .HasColumnType("money");

            builder.Property(ci => ci.Region)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ci => ci.Adresse)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(ci => ci.Bedroom)
                .WithMany()
                .HasForeignKey(ci => ci.BedroomId);

            builder.HasOne(ci => ci.Country)
                .WithMany()
                .HasForeignKey(ci => ci.CountryId);

            builder.HasOne(ci => ci.Furniture)
                .WithMany()
                .HasForeignKey(ci => ci.FurnitureId);

            builder.HasOne(ci => ci.Period)
                .WithMany()
                .HasForeignKey(ci => ci.PeriodId);
        }
    }
}
