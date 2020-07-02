using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Messages
{
    public class GetGrantsListCommand: MessageBase
    {
        public string Key { get; set; }

        public string SubjectId { get; set; }

        public string ClientId { get; set; }

        public string Type { get; set; }
    }
}
