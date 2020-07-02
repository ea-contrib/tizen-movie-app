using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMA.Data.Common;

namespace TMA.MovieService.Entities
{
    [Table("Movie")]
    public class MovieEntity: IEntity, IIdentitySupporter<int>
    {
        [Key]
        public int Id { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }
        
        public int ReleaseYear { get; set; }

        public string ExternalId { get; set; }

        public string ExternalId2 { get; set; }

        public int Type { get; set; }

        public double Rating { get; set; }

        public DateTime UpdateTime { get; set; }

        public double Popularity { get; set; }

        public string Country { get; set; }

        public string ProviderId { get; set; }
        
        // public List<int> Genres { get; set; }
    }
}
