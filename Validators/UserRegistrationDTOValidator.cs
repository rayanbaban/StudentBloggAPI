using FluentValidation;
using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Validators;

public class UserRegistrationDTOValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username kan ikke være null")
            .MinimumLength(3).WithMessage("Username må ha minst 3 tegn")
            .MaximumLength(10).WithMessage("Username kan ikke ha flere en 10 tegn");

        //RuleFor(x => x.Email)
        //    .NotEmpty().WithMessage("Email må være med.")
        //    .EmailAddress().WithMessage("Må ha en gyldig email adresse.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname kan ikke være null");

		RuleFor(x => x.LastName)
			.NotEmpty().WithMessage("Lastname kan ikke være null");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Passord må være med")
            .MinimumLength(8).WithMessage("Passordet må inneholde minst 8 tegn")
            .MaximumLength(16).WithMessage("Passordet kan ikke inneholde mer en 16 tegn")
            .Matches(".*[0-9]+.*").WithMessage("Må ha minst 1 tall i passordet")
            .Matches(@"[A-Z]+").WithMessage("Må ha minst en stor bokstav")
            .Matches(@"[a-z]+").WithMessage("Må ha minst en liten bokstav")
            .Matches(@"[!?*#_+]").WithMessage("Må ha minst et spesial tegn");
	}
}
