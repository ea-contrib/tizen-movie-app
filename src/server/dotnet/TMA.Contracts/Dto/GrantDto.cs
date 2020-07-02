﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Contracts.Dto
{
    public class GrantDto
    {

        public string Key { get; set; }

        public string Type { get; set; }

        public string SubjectId { get; set; }

        public string ClientId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? Expiration { get; set; }

        public string Data { get; set; }
    }
}
