using Assignment2.Application.DTOs.Projects;
using FluentValidation;

namespace Assignment2.Application.Validators.Projects
{
    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.");
        }
    }
}
