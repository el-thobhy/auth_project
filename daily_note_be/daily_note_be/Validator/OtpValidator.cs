using Daily_Note.Models;
using FluentValidation;
using ViewModel;

namespace Daily_Note.Validator
{
    public class OtpValidator : AbstractValidator<AccountViewModel>
    {
        public OtpValidator(DailyNoteDbContext dbContext)
        {
            RuleFor(x => x.Otp)
                .Must(Otp =>
                {
                    return dbContext.Accounts.Where(o => o.Otp == Otp).Any();
                })
                .WithName("Otp")
                .WithMessage("Otp tidak Sesuai");
        }
    }
}
