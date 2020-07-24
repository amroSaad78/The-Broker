using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    internal class PurposeEntityTypeConfiguration : IEntityTypeConfiguration<Purpose>
    {
        public void Configure(EntityTypeBuilder<Purpose> builder)
        {
            builder.ToTable("Purpose");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("purpose_hilo")
               .IsRequired();

            builder.Property(cb => cb.PurposeType)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}