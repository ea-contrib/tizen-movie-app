using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Dto
{
    public class ClaimsDto
    {
        public List<Tuple<string, string, string>> Claims { get; set; }
    }
}
