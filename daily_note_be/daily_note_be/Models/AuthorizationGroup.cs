using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Daily_Note.Models
{
    [Table("m_authorization_groups")]
    public class AuthorizationGroup : BaseProperties
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int RoleGroupId { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }

        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
