using BookStore.MessageQueue;
using BookStore.RedisMessageQueue.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.RedisMessageQueue.DI
{
    public static class RedisTypeInstaller
    {
        public static void AddRedisMQType(this IServiceCollection services, string redisServerEndpoint)
        {
            services.AddSingleton<IRedisStore>(new RedisStore(redisServerEndpoint));
            services.AddTransient<IPublisher<PurchaseBookRedisModel, long>, RedisPublisher<PurchaseBookRedisModel>>();
            services.AddTransient<ISubscriber<PurchaseBookRedisModel>, RedisSubscriber<PurchaseBookRedisModel>>();            
        }
    }
}
