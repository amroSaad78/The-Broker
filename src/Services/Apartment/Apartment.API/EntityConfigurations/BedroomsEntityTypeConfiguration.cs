using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    internal class BedroomsEntityTypeConfiguration : IEntityTypeConfiguration<Bedrooms>
    {
        public void Configure(EntityTypeBuilder<Bedrooms> builder)
        {
            builder.ToTable("Bedroom");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("bedroom_hilo")
               .IsRequired();

            builder.Property(cb => cb.BedroomsCount)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}