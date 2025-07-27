using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly LocalHaulDbContext _dbContext;

        public MessageService(LocalHaulDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SendMessageAsync(Message message)
        {
            // Ensure message has a unique ID if not already set
            if (message.Id == Guid.Empty)
            {
                message.Id = Guid.NewGuid();
            }
            message.SentDate = DateTime.UtcNow; // Ensure sent date is set to UTC now

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Message>> GetConversationAsync(string user1Id, string user2Id, Guid? productId)
        {
            IQueryable<Message> query = _dbContext.Messages
                                                  .Include(m => m.Sender)
                                                  .Include(m => m.Receiver)
                                                  .Include(m => m.Product);

            // Filter by participation in the conversation
            query = query.Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                                     (m.SenderId == user2Id && m.ReceiverId == user1Id));

            // Filter by product if provided
            if (productId.HasValue && productId != Guid.Empty)
            {
                query = query.Where(m => m.ProductId == productId.Value);
            }

            return await query.OrderBy(m => m.SentDate).ToListAsync(); // Order chronologically
        }

        public async Task<IEnumerable<Message>> GetReceivedMessagesAsync(string receiverId)
        {
            return await _dbContext.Messages
                                   .Where(m => m.ReceiverId == receiverId)
                                   .Include(m => m.Sender)   // Eagerly load the sender
                                   .Include(m => m.Receiver) // Eagerly load the receiver
                                   .Include(m => m.Product)  // Eagerly load the product (if any)
                                   .OrderByDescending(m => m.SentDate)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetSentMessagesAsync(string senderId)
        {
            return await _dbContext.Messages
                                   .Where(m => m.SenderId == senderId)
                                   .Include(m => m.Sender)
                                   .Include(m => m.Receiver)
                                   .Include(m => m.Product)
                                   .OrderByDescending(m => m.SentDate)
                                   .ToListAsync();
        }

    }
}
