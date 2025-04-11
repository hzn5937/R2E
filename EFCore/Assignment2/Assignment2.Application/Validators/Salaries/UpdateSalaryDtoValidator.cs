using Assignment2.Application.DTOs.Salaries;
using FluentValidation;

namespace Assignment2.Application.Validators.Salaries
{
    internal class UpdateSalaryDtoValidator : AbstractValidator<UpdateSalaryDto>
    {
        public UpdateSalaryDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive amount.")
                .LessThanOrEqualTo(1000000).WithMessage("Salary must be less than or equal to 1,000,000.");
        }
    }
}
