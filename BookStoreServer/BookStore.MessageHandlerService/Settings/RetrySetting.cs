using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.MessageHandlerService.Settings
{
    public class RetrySetting
    {
        public int Limit { get; set; }
        public int DelayInSeconds { get; set; }
    }
}
