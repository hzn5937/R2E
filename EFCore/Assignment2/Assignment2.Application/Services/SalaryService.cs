using Assignment2.Application.DTOs.Salaries;
using Assignment2.Application.Extensions;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public SalaryService(ISalaryRepository salaryRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<SalaryOutputDto> CreateAsync(CreateSalaryDto salaryDto)
        {
            if (await EmployeeExist(salaryDto.EmployeeId) == false)
            {
                throw new NotFoundException($"Employee with ID {salaryDto.EmployeeId} not found.");
            }

            var salary = _mapper.Map<Salaries>(salaryDto);

            var createdSalary = await _salaryRepository.CreateAsync(salary);

            var output = _mapper.Map<SalaryOutputDto>(createdSalary);

            return output;
        }

        public async Task<IEnumerable<SalaryOutputDto>> GetAllAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<SalaryOutputDto>>(salaries);
            return output;
        }

        public async Task<SalaryOutputDto?> GetByIdAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            if (salary == null)
            {
                return null;
            }
            var output = _mapper.Map<SalaryOutputDto?>(salary);
            return output;
        }

        public async Task<SalaryOutputDto?> UpdateAsync(UpdateSalaryDto salaryDto)
        {
            if (await EmployeeExist(salaryDto.EmployeeId) == false)
            {
                throw new NotFoundException($"Employee with ID {salaryDto.EmployeeId} not found.");
            }

            var salary = _mapper.Map<Salaries>(salaryDto);

            var updatedSalary = await _salaryRepository.UpdateAsync(salary);

            var output = _mapper.Map<SalaryOutputDto>(updatedSalary);

            return output;
        }

        // helper functions
        private async Task<bool> EmployeeExist(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            return employee != null;
        }

        public async Task<bool> DeleteAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);  

            if (employee == null)
            {
                return false;
            }

            await _salaryRepository.DeleteAsync(employeeId);
            return true;
        }
    }
}
