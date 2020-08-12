using BookStore.MessageQueue;
using System.Collections.Generic;

namespace BookStore.RedisMessageQueue.Models
{
    public class PurchaseBookRedisModel : IMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string PublishedDate { get; set; }
        public string Subtitle { get; set; }
        public System.DateTime CreatedDateTime { get; set; }

        public string QueueName => "BookStore.RedisMessageQueue.Models.PurchaseBookRedisModel";
    }
    
}
