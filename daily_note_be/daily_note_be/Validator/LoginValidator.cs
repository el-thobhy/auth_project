using Daily_Note.Models;
using FluentValidation;
using ViewModel;

namespace Daily_Note.Validator
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator(DailyNoteDbContext dbContext)
        {
            RuleFor(x => x.UserName)
                .Must(username =>
                {
                    return dbContext.Accounts.Where(o => o.UserName == username).Any();
                })
                .WithName("UserName")
                .WithMessage("Username Tidak Ditemukan");
        }
    }
}
