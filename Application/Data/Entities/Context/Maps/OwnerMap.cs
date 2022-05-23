using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Context.Maps
{
    public class OwnerMap : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Document).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.Cep).IsRequired();
                address.Property(x => x.State).IsRequired();
                address.Property(x => x.City).IsRequired();
                address.Property(x => x.Neighborhood).IsRequired();
                address.Property(x => x.Street).IsRequired();
                address.Property(x => x.Service).IsRequired();
            });
            builder.Property(x => x.Status).IsRequired();


            builder.HasIndex(x => x.Document).IsUnique();
        }
    }
}
