using Daily_Note.Models;
using FluentValidation;

namespace Daily_Note.Validator
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator(DailyNoteDbContext dbContext)
        {
            RuleFor(x => x)
                .Must(email =>
                {
                    return dbContext.Accounts.Where(o => o.Email == email).Any();
                })
                .WithName("Email")
                .WithMessage("Email Tidak Ditemukan");
        }
    }
}
