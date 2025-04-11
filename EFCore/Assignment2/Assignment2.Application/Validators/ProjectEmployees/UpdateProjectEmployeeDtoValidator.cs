using Assignment2.Application.DTOs.ProjectEmployees;
using FluentValidation;

namespace Assignment2.Application.Validators.ProjectEmployees
{
    public class UpdateProjectEmployeeDtoValidator : AbstractValidator<UpdateProjectEmployeeDto>
    {
        public UpdateProjectEmployeeDtoValidator()
        {
            RuleFor(x => x.Enable)
                .NotNull().WithMessage("Enable must be specified.");
        }
    }
}
