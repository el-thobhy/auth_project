using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ViewModel;
using FluentValidation.Results;
using Daily_Note.Repositories;
using Daily_Note.Models;
using Daily_Note.Security;

namespace Daily_Note.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private AccountRepositories _repo;
        private IValidator<LoginViewModel> _validator;
        private IValidator<string> _validatorEmail;
        private IValidator<RegisterViewModel> _validatorRegis;
        private IValidator<UpdateProfileViewModel> _validatorUpdate;
        private IValidator<AccountViewModel> _validatorOtp;

        public AccountController(
            DailyNoteDbContext dbContext,
            IConfiguration configuration,
            IValidator<LoginViewModel> validator,
            IValidator<string> validatorEmail,
            IValidator<AccountViewModel> validatorOtp,
            IValidator<RegisterViewModel> validatorRegis,
            IValidator<UpdateProfileViewModel> validatorUpdate
            )
        {
            _repo = new AccountRepositories(dbContext, configuration);
            _configuration = configuration;
            _validator = validator;
            _validatorEmail = validatorEmail;
            _validatorOtp = validatorOtp;
            _validatorRegis = validatorRegis;
            _validatorUpdate = validatorUpdate;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ResponseResult response = _repo.Login(model);
            AccountViewModel result = (AccountViewModel) response.Data;
            ValidationResult validate = await _validator.ValidateAsync(model);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            else
            {
                if (response.Success)
                {
                    if (result.UserName != null)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.UserName),
                        new Claim("FirstName", result.FirstName),
                        new Claim("LastName", result.LastName)
                    };
                        foreach (var role in result.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var token = GetToken(claims);
                        result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(response);
                    }
                    else
                    {
                        List<object> resultError = new List<object>
                        {
                            new
                            {
                                propertyName = "Password",
                                errorMessage = "Password Salah"
                            }
                        };
                        return NotFound(resultError);
                    }
                }
                else
                {
                    return NotFound(response);
                }
            }
        }

        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:Expires"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> Post(SendOtpViewModel otp)
        {
            ResponseResult response = _repo.SendOtp(otp.IsRegistration, otp.Email);
            OtpViewModel result = (OtpViewModel) response.Data;
            if (otp.IsRegistration == false)
            {
                ValidationResult validate = await _validatorEmail.ValidateAsync(otp.Email);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                else
                {
                    if (result.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(response);
                    }
                }
            }
            else
            {
                RegisterViewModel model = new RegisterViewModel
                {
                    Email = otp.Email
                };
                ValidationResult validate = await _validatorRegis.ValidateAsync(model);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                else
                {
                    if (result.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(response);
                    }
                }
            }
        }

        [HttpPost("VerifikasiOtp")]
        public async Task<IActionResult> Verifikasi(VerificationOtpViewModel ver)
        {
            ResponseResult response = _repo.VerifikasiOtp(ver.Otp);
            OtpViewModel result = (OtpViewModel) response.Data;
            AccountViewModel account = new AccountViewModel();
            account.Otp = ver.Otp;
            ValidationResult validate = await _validatorOtp.ValidateAsync(account);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            else
            {
                if (result.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }
            }
        }
        [HttpPost("UbahPassword")]
        public async Task<IActionResult> UbahPassword(ChangePwdViewModel chg)
        {
            ResponseResult response = _repo.UbahPassword(chg.UserName, chg.Password);
            ChangePasswordViewModel result = (ChangePasswordViewModel) response.Data;
            if (result.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            ValidationResult validate = await _validatorRegis.ValidateAsync(model);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            else
            {
                return Ok(_repo.Register(model));
            }
        }
        [HttpPost("UpdateProfile")]
        [ReadableBodyStream(Roles = "Administrator,home")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            ValidationResult validate = await _validatorUpdate.ValidateAsync(model);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors);
            }
            else
            {
                return Ok(_repo.UpdateProfile(model));
            }
        }
    }
}
