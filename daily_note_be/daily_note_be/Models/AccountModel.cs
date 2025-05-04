using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Daily_Note.Models
{
    [Table("m_accounts")]
    public class Account : BaseProperties
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(200)]
        public string Email { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpExpire { get; set; }
        public int Attempt { get; set; }
        public int RoleGroupId { get; set; }

        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
