using Data.Models;
using LocalHaul.GlobalCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Category.NameMaxLength)
                .HasComment("The name of the product category (e.g., 'Electronics').");

            builder.HasIndex(c => c.Name)
                .IsUnique(); // Ensure category names are unique
        }
    }
}
