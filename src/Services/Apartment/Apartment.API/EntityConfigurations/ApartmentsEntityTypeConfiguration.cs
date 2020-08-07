using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    public class ApartmentsEntityTypeConfiguration : IEntityTypeConfiguration<Model.Apartment>
    {
        public void Configure(EntityTypeBuilder<Model.Apartment> builder)
        {
            builder.ToTable("Apartment");

            builder.Property(ci => ci.Id)
                .UseHiLo("Apartment_hilo")
                .IsRequired();

            builder.Property(ci => ci.OwnerId)
                .HasDefaultValueSql("0")
                .IsRequired();

            builder.Property(ci => ci.View)
                .HasMaxLength(50);

            builder.Property(ci => ci.City)
                .HasMaxLength(50)
                .IsRequired();

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
