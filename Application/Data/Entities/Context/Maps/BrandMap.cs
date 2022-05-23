using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Context.Maps
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(GetBrands());
        }

        private static Brand[] GetBrands()
            => new Brand[] {
                new Brand { Id = 1, Name = "BMW", Status = StatusBrand.ACTIVE },
                new Brand { Id = 2, Name = "MERCEDES", Status = StatusBrand.ACTIVE },
                new Brand { Id = 3, Name = "AUDI", Status = StatusBrand.ACTIVE },
                new Brand { Id = 4, Name = "CHEVROLET", Status = StatusBrand.CANCELED },
                new Brand { Id = 5, Name = "VOLKSWAGEN", Status = StatusBrand.CANCELED },
            };
    }
}
