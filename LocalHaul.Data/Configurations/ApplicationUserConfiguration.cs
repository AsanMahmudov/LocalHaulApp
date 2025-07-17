using Data.Models;
using LocalHaul.GlobalCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LocalHaul.GlobalCommon.ValidationConstants.ApplicationUser;
namespace Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(ValidationConstants.ApplicationUser.FirstNameMaxLength)
                .HasComment("The first name of the user.");

            builder.Property(u => u.LastName)
                .HasMaxLength(ValidationConstants.ApplicationUser.LastNameMaxLength)
                .HasComment("The last name of the user.");

            builder.Property(u => u.City)
                .HasMaxLength(ValidationConstants.ApplicationUser.CityMaxLength)
                .HasComment("The city where the user is located.");
        }
    }
}
