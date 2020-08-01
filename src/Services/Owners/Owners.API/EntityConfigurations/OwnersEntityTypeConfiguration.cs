using Owners.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Owners.API.EntityConfigurations
{
    public class OwnersEntityTypeConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owner");

            builder.Property(ci => ci.Id)
                .UseHiLo("Owner_hilo")
                .IsRequired();

            builder.Property(ci => ci.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ci => ci.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ci => ci.Email)
                .HasMaxLength(50);

            builder.Property(ci => ci.Mobile)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(ci => ci.City)
                .HasMaxLength(50);

            builder.Property(ci => ci.Company)
                .HasMaxLength(50);

            builder.Property(ci => ci.Address)
                .HasMaxLength(100);

            builder.Property(ci => ci.ZIP)
                .HasMaxLength(5);

        }
    }
}
