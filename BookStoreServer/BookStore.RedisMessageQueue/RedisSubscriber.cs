using BookStore.MessageQueue;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BookStore.RedisMessageQueue
{
    public class RedisSubscriber<TMessage> : ISubscriber<TMessage>
    {
        private StackExchange.Redis.ISubscriber _subscriber;
        protected readonly IRedisStore _redisStore;
        public string LogPrefix { get; set; }

        public RedisSubscriber(IRedisStore redisStore)
        {
            _redisStore = redisStore;
            LogPrefix = this.GetType().Name;
        }

        public async Task SubscribeAsync(string queueName, Action<TMessage> handler)
        {
            _subscriber = _redisStore.GetSubscriber();
            await _subscriber.SubscribeAsync(queueName, (channel, rawMessage) =>
            {
                var message = JsonConvert.DeserializeObject<TMessage>(rawMessage);
                handler(message);
            });
        }

        public void Unsubscribe(string queueName)
        {
            if(_subscriber != null)
            {
                _subscriber.Unsubscribe(queueName);
            }
        }
    }
}
