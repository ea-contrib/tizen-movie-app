using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Messages
{
    public class IsPrincipalCredentialsValidCommand: MessageBase
    {
        public string Email { get; set; }
        public string Password{ get; set; }
    }
}
