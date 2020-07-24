using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    internal class FurnishingsEntityTypeConfiguration : IEntityTypeConfiguration<Furnishings>
    {
        public void Configure(EntityTypeBuilder<Furnishings> builder)
        {
            builder.ToTable("Furniture");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("furniture_hilo")
               .IsRequired();

            builder.Property(cb => cb.FurnitureType)
                .IsRequired()
                .HasMaxLength(100);
        }
        
    }
}