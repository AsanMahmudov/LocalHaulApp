using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Defines the contract for message-related business operations.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Sends a new message from one user to another.
        /// </summary>
        /// <param name="message">The Message entity to send.</param>
        Task SendMessageAsync(Message message);

        /// <summary>
        /// Retrieves all messages received by a specific user.
        /// </summary>
        /// <param name="receiverId">The ID of the user who received the messages.</param>
        /// <returns>A collection of received Message entities.</returns>
        Task<IEnumerable<Message>> GetReceivedMessagesAsync(string receiverId);

        /// <summary>
        /// Retrieves all messages sent by a specific user.
        /// </summary>
        /// <param name="senderId">The ID of the user who sent the messages.</param>
        /// <returns>A collection of sent Message entities.</returns>
        Task<IEnumerable<Message>> GetSentMessagesAsync(string senderId);

        /// <summary>
        /// Retrieves messages forming a conversation between two specific users regarding a product.
        /// (This could be expanded for more complex conversation threading)
        /// </summary>
        /// <param name="user1Id">The ID of the first user in the conversation.</param>
        /// <param name="user2Id">The ID of the second user in the conversation.</param>
        /// <param name="productId">Optional: The ID of the product the conversation is about.</param>
        /// <returns>A collection of messages exchanged between the two users for the given product.</returns>
        Task<IEnumerable<Message>> GetConversationAsync(string user1Id, string user2Id, Guid? productId);
    }
}
