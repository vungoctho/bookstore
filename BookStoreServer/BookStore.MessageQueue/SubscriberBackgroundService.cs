using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.MessageQueue
{
    public abstract class SubscriberBackgroundService<TMessage> : BackgroundService where TMessage : IMessage, new()
    {        
        protected readonly ILogger _logger;
        protected ISubscriber<TMessage> _subscriber;
        private int _retryDelay = 0;
        private bool _abort = false;

        public event Action OnAbort;
        public int Tries { get; private set; }
        public string LogPrefix { get; set; }

        public SubscriberBackgroundService(ILogger logger, ISubscriber<TMessage> subscriber)
        {
            _subscriber = subscriber;
            _logger = logger;
            LogPrefix = this.GetType().Name;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LogPrefix} running at: {DateTimeOffset.Now}");
            
            var messageType = new TMessage();
            try
            {
                await _subscriber.SubscribeAsync(messageType.QueueName, (message) =>
                {
                    Tries = 0;
                    do
                    {
                        Tries++;
                        _retryDelay = 0;

                        ProcessMessage(message);

                        if (_retryDelay > 0)
                        {
                            cancellationToken.WaitHandle.WaitOne(_retryDelay * 1000);
                        }
                    }
                    while (!_abort && _retryDelay > 0);

                });

                while (!cancellationToken.IsCancellationRequested && !_abort)
                {
                    await Task.Delay(1000, cancellationToken);
                }
                if (_abort)
                {
                    OnAbort?.Invoke();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Unhanlded error: {ex.Message}", ex);
                OnAbort?.Invoke();
            }
            finally
            {
                _subscriber.Unsubscribe(messageType.QueueName);
            }
        }

        protected abstract void ProcessMessage(TMessage message);

        /// <summary>
        /// Call this function if there is error exception and want to retry to process message
        /// </summary>
        /// <param name="retryDelay"></param>
        public void SetRetryAfterInSenconds(int retryDelay)
        {
            _retryDelay = retryDelay;
        }

        /// <summary>
        /// Abort process message and stop the background service
        /// </summary>
        public void Abort()
        {
            _abort = true;
        }
    }
}
