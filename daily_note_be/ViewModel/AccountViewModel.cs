using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class LoginViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }
    }
    public class ChangePwdViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }
    }
    public class SendOtpViewModel
    {
        [Required]
        public bool IsRegistration { get; set; }

        [Required, MaxLength(200)]
        public string Email { get; set; }
    }
    public class VerificationOtpViewModel
    {
        [Required]
        public string Otp { get; set; }
    }

    public class AccountViewModel
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Is_delete { get; set; }
        public string Otp { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
    public class RegisterViewModel : LoginViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(200)]
        public string Email { get; set; }

        public int RoleGroupId { get; set; }
    }

    public class UpdateProfileViewModel {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress), MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }

    }
}
