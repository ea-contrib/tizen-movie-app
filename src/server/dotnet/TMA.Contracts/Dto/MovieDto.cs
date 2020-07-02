using System;
using System.Collections.Generic;
using System.Text;
using TMA.Contracts.Enums;

namespace TMA.Contracts.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public int ReleaseYear { get; set; }

        public string ExternalId { get; set; }

        public string ExternalId2 { get; set; }

        public MovieType Type { get; set; }

        public double Rating { get; set; }

        public DateTime UpdateTime { get; set; }

        public double Popularity { get; set; }

        public string Country { get; set; }

        public string ProviderId { get; set; }
    }
}
