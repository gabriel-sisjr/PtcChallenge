using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Context.Maps
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Renavam).IsRequired();
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.YearCreation).IsRequired();
            builder.Property(x => x.YearModel).IsRequired();
            builder.Property(x => x.Quilometers).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(x => x.Brand).WithMany().HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId);

            builder.HasIndex(x => x.Renavam).IsUnique();
        }
    }
}
