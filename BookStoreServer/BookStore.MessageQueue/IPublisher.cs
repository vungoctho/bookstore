using System;
using System.Threading.Tasks;

namespace BookStore.MessageQueue
{
    public interface IPublisher<TInput, TOutput> : IDisposable
    {
        /// <summary>
        /// Asynchronously publish a single message to the queue 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<TOutput> PublishAsync(string queueName, TInput message);

        /// <summary>
        /// Asynchronously publish a single message to the queue 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<TOutput> PublishAsync(TInput message);

        /// <summary>
        /// Publish message to queue synchronously
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        /// <param name="action">Callback action</param>
        void Publish(string queueName, TInput message, Action<TOutput> action = null);

        /// <summary>
        /// Publish message to queue synchronously
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action">Callback action</param>
        void Publish(TInput message, Action<TOutput> action = null);
    }
}
