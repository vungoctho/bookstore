using BookStore.RedisMessageQueue.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace BookStore.MessageHandlerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddRedisMQType(hostContext.Configuration.GetValue<string>("Redis:Endpoint"));
                    services.AddHostedService<PurchaseBookSubscriber>();

                })
            .UseWindowsService();

       

    }
}
