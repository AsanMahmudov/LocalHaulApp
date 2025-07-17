using Data.Models;
using LocalHaul.GlobalCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {           

            builder.Property(i => i.ImagePath)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Image.ImagePathMaxLength)
                .HasComment("The file path or URL where the image is stored.");

            builder.Property(i => i.ProductId)
                .IsRequired()
                .HasComment("Foreign key to the Product table this image belongs to.");

            builder.HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Delete images if product is deleted
        }
    }
}
