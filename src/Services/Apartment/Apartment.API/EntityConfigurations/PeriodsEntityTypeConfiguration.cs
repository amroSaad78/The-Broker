using Apartment.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apartment.API.EntityConfigurations
{
    internal class PeriodsEntityTypeConfiguration : IEntityTypeConfiguration<Periods>
    {
        public void Configure(EntityTypeBuilder<Periods> builder)
        {
            builder.ToTable("Period");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("period_hilo")
                .IsRequired();

            builder.Property(cb => cb.Period)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}