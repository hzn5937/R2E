using Assignment2.Application.DTOs.Salaries;

namespace Assignment2.Application.Interfaces
{
    public interface ISalaryService
    {
        Task<SalaryOutputDto> CreateAsync(CreateSalaryDto salaryDto);
        Task<IEnumerable<SalaryOutputDto>> GetAllAsync();
        Task<SalaryOutputDto?> GetByIdAsync(int id);
        Task<SalaryOutputDto?> UpdateAsync(UpdateSalaryDto salaryDto);
        Task<bool> DeleteAsync(int id);
    }
}
