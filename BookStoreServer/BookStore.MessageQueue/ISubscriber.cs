using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.MessageQueue
{
    public interface ISubscriber<TMessage>
    {
        Task SubscribeAsync(string queueName, Action<TMessage> handler);

        void Unsubscribe(string queueName);
    }
}
