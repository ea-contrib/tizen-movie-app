using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Messages
{
    public class ResponseMessage
    {
    }
    public class ResponseMessage<TValue>
    {
        public TValue Value { get; set; }
    }
}
