using StackExchange.Redis;
using System;

namespace BookStore.RedisMessageQueue
{
    public interface IRedisStore
    {
        StackExchange.Redis.ISubscriber GetPublisher();
        StackExchange.Redis.ISubscriber GetSubscriber();
    }
    public class RedisStore : IRedisStore
    {
        private readonly Lazy<ConnectionMultiplexer> LazyConnection;

        public RedisStore(string endpoint)
        {
            var configurationOptions = new ConfigurationOptions()
            {
                EndPoints = { endpoint }
            };
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public ConnectionMultiplexer Connection => LazyConnection.Value;
        public IDatabase RedisCache => Connection.GetDatabase();

        public ISubscriber GetPublisher()
        {
            return RedisCache.Multiplexer.GetSubscriber();
        }

        public ISubscriber GetSubscriber()
        {
            return RedisCache.Multiplexer.GetSubscriber();
        }
    }
}
