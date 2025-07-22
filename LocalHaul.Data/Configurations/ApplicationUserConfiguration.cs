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

            // Soft Delete Configuration for ApplicationUser
            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Indicates if the user account is soft-deleted.");

            // Global Query Filter: Exclude soft-deleted users by default
            builder.HasQueryFilter(u => !u.IsDeleted);

            // Explicitly define the inverse relationships for Messages to resolve ambiguity
            // This tells EF Core that ApplicationUser.SentMessages is the "one" side of the relationship
            // where Message.Sender is the inverse navigation property.
            builder.HasMany(u => u.SentMessages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of sender if messages exist

            // This tells EF Core that ApplicationUser.ReceivedMessages is the "one" side of the relationship
            // where Message.Receiver is the inverse navigation property.
            builder.HasMany(u => u.ReceivedMessages)
                   .WithOne(m => m.Receiver)
                   .HasForeignKey(m => m.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of receiver if messages exist
        }
    }
}
