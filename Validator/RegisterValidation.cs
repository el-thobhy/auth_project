using Daily_Note.Models;
using FluentValidation;
using ViewModel;

namespace Daily_Note.Validator
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator(DailyNoteDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .Must(email =>
                {
                    return !dbContext.Accounts.Where(o => o.Email == email).Any();
                })
                .WithName("Email")
                .WithMessage("Email Sudah Ada, Silahkan Klik Lupa Password");

            RuleFor(x => x.UserName)
            .Must(username =>
            {
                return !dbContext.Accounts.Where(o => o.UserName == username).Any();
            })
            .WithName("UserName")
            .WithMessage("Username Sudah digunakan");
        }
    }
}
