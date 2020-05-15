using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Messages
{
    public class GetPrincipalClaimsCommand: MessageBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
