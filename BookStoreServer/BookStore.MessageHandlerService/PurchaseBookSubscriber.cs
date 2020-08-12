using BookStore.MessageHandlerService.Settings;
using BookStore.MessageQueue;
using BookStore.RedisMessageQueue.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace BookStore.MessageHandlerService
{
    public class PurchaseBookSubscriber : SubscriberBackgroundService<PurchaseBookRedisModel>
    {
        
        private readonly RetrySetting _retrySetting;
        public PurchaseBookSubscriber(ILogger<PurchaseBookSubscriber> logger,            
            IOptions<RetrySetting> retrySetting,
            ISubscriber<PurchaseBookRedisModel> subscriber)
            : base(logger, subscriber)
        {
            _retrySetting = retrySetting.Value;
        }

        protected override void ProcessMessage(PurchaseBookRedisModel message)
        {
            try
            {
                _logger.LogInformation($"{LogPrefix} - Receive Message from bus: {JsonConvert.SerializeObject(message)}");
                // do some next steps, such as Persist to DB and produce another message (e.g. Payment)
            }
            catch (Exception ex)
            {
                if (Tries < _retrySetting.Limit && IsRetryException(ex))
                {
                    _logger.LogWarning($"{LogPrefix} - Retry {Tries} - {ex.Message}", ex);
                    SetRetryAfterInSenconds(_retrySetting.DelayInSeconds);
                }
                else
                {
                    _logger.LogError($"{LogPrefix} - {ex.Message}", ex);
                }
            }
        }

        protected bool IsRetryException(Exception ex)
        {
            // some exception need to retry e.g. SQL Connection, API Timeout

            return false;
        }
    }
}
