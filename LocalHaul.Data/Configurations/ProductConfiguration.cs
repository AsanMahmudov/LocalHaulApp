using Data.Models;
using LocalHaul.GlobalCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LocalHaul.GlobalCommon.ValidationConstants.Product;

namespace Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasComment("Represents a product listing posted by a user for sale.");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Product.TitleMaxLength)
                .HasComment("The title of the product listing.");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Product.DescriptionMaxLength)
                .HasComment("Detailed description of the product.");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType(ValidationConstants.Product.PriceColumnType)
                .HasComment("The selling price of the product.");

            builder.Property(p => p.PostedDate)
                .IsRequired()
                .HasComment("The date and time (UTC) when the product listing was posted.");

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasComment("Foreign key to the Category table.");

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent category deletion if products exist

            builder.Property(p => p.SellerId)
                .IsRequired()
                .HasComment("Foreign key to the ApplicationUser (seller) who posted the product.");

            builder.HasOne(p => p.Seller)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Soft Delete Configuration for Product
            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Indicates if the product listing has been soft-deleted.");

            // Global Query Filter: Exclude soft-deleted products by default
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
