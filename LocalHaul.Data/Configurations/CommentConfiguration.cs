using Data.Models;
using LocalHaul.GlobalCommon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Message.ContentMaxLength)
                .HasComment("The actual content of the message.");

            builder.Property(m => m.SentDate)
                .IsRequired()
                .HasComment("The date and time (UTC) when the message was sent.");

            builder.Property(m => m.SenderId)
                .IsRequired()
                .HasComment("Foreign key to the ApplicationUser who sent the message.");

            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent sender deletion if messages exist

            builder.Property(m => m.ReceiverId)
                .IsRequired()
                .HasComment("Foreign key to the ApplicationUser who received the message.");

            builder.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent receiver deletion if messages exist

            builder.Property(m => m.ProductId)
                .HasComment("Optional foreign key to the Product this message is related to.");

            builder.HasOne(m => m.Product)
                .WithMany() // No navigation property back from Product to Message for simplicity
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.SetNull); // Set ProductId to null if product is deleted
        }
    }
}
