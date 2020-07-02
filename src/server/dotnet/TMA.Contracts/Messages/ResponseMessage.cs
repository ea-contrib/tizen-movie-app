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
        public ResponseMessage() { }
        public ResponseMessage(TValue value)
        {
            Value = value;
        }
        public TValue Value { get; set; }
    }
}
