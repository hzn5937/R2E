using Assignment2.Application.DTOs.Person;
using FluentValidation;

namespace Assignment2.Application.Validators.Person
{
    public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20).WithMessage("First name is required and cannot exceed 20 characters.");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20).WithMessage("Last name is required and cannot exceed 20 characters.");
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth is required and must be in the past.");
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.BirthPlace).NotEmpty();
        }
    }
}
