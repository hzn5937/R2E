using Assignment2.Application.DTOs.ProjectEmployees;
using FluentValidation;

namespace Assignment2.Application.Validators.ProjectEmployees
{
    public class CreateProjectEmployeeDtoValidator : AbstractValidator<CreateProjectEmployeeDto>
    {
        public CreateProjectEmployeeDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.Enable)
                .NotNull().WithMessage("Enable must be specified.");
        }
    }
}
