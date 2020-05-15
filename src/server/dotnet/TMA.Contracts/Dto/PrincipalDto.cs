using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Dto
{
    public class PrincipalDto: DtoBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
