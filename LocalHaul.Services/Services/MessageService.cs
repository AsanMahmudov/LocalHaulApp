using Data.Models;
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
        public Task<IEnumerable<Message>> GetConversationAsync(string user1Id, string user2Id, Guid? productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetReceivedMessagesAsync(string receiverId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetSentMessagesAsync(string senderId)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
