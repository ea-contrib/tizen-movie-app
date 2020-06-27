using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMA.Data.Common;

namespace TMA.PrincipalService.Entities
{
    [Table("Principal")]
    public class PrincipalEntity: IIdentitySupporter<int>, IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
