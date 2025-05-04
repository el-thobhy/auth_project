using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Daily_Note.Models
{
    [Table("m_role_group")]
    public class RoleGroup : BaseProperties
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string GroupName { get; set; }

        public ICollection<AuthorizationGroup> AuthorizationGroups { get; set; }
    }
}
