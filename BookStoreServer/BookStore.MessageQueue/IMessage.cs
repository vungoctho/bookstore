using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.MessageQueue
{
    public interface IMessage
    {
        string QueueName { get; }
    }
}
