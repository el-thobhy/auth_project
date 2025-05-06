using Daily_Note.Models;
using FluentValidation;
using ViewModel;

namespace daily_note_be.Validator
{
    public class UpdateProfileValidator: AbstractValidator<UpdateProfileViewModel>
    {
        public UpdateProfileValidator(DailyNoteDbContext dbContext) {
            RuleFor(x => x.Email)
                .Must(email =>
                {
                    return !dbContext.Accounts.Where(o => o.Email == email).Any();
                })
                .WithName("Email")
                .WithMessage("Email Sudah Digunakan");

            RuleFor(x => x.UserName)
                .Must(username =>
                {
                    return !dbContext.Accounts.Where(o => o.UserName == username).Any();
                })
                .WithName("UserName")
                .WithMessage("Username Sudah Digunakan");
        }
    }
}
