using BookStore.MessageQueue;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BookStore.RedisMessageQueue
{
    public class RedisPublisher<TInput> : IPublisher<TInput, long> where TInput : IMessage, new()
    {
        private readonly IRedisStore _redisStore;
        public RedisPublisher(IRedisStore redisStore)
        {
            _redisStore = redisStore;
        }
        public Task<long> PublishAsync(string queueName, TInput message)
        {
            var publisher = _redisStore.GetPublisher();
            return publisher.PublishAsync(queueName, JsonConvert.SerializeObject(message));
        }

        public Task<long> PublishAsync(TInput message)
        {
            return PublishAsync(message.QueueName, message);
        }

        public void Publish(string queueName, TInput message, Action<long> action = null)
        {
            var publisher = _redisStore.GetPublisher();
            var ret = publisher.Publish(queueName, JsonConvert.SerializeObject(message));
            if (action != null)
            {
                action.Invoke(ret);
            }
        }

        public void Publish(TInput message, Action<long> action = null)
        {
            Publish(message.QueueName, message, action);
        }

        public void Dispose()
        {
        }
    }
}
