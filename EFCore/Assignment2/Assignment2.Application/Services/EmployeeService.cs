using Assignment2.Application.DTOs.Employees;
using Assignment2.Application.Extensions;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Domain.Models;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository ,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            
        }

        public async Task<EmployeeOutputDto> CreateAsync (CreateEmployeeDto createEmployeeDto)
        {
            if (await DepartmentExist(createEmployeeDto.DepartmentId) == false)
            {
                throw new NotFoundException($"Department with id {createEmployeeDto.DepartmentId} does not exist");
            }

            var employee = _mapper.Map<Employees>(createEmployeeDto);

            var createdEmployee = await _employeeRepository.CreateAsync(employee);

            var output = _mapper.Map<EmployeeOutputDto>(createdEmployee);

            return output;
        }

        public async Task<EmployeeOutputDto?> GetByIdAsync(int id)
        {
           var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            var output = _mapper.Map<EmployeeOutputDto?>(employee);

            return output;
        }

        public async Task<IEnumerable<EmployeeOutputDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var output = _mapper.Map<IEnumerable<EmployeeOutputDto>>(employees);

            return output;
        }

        public async Task<EmployeeOutputDto?> UpdateAsync(int id, UpdateEmployeeDto employeeDto)
        {
            if (await DepartmentExist(employeeDto.DepartmentId) == false)
            {
                throw new NotFoundException($"Department with id {employeeDto.DepartmentId} does not exist");
            }

           var employee = _mapper.Map<Employees>(employeeDto);

            employee.Id = id;

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);

            var output = _mapper.Map<EmployeeOutputDto>(updatedEmployee);

            return output;
        }

        public async Task<bool> DeleteAsync (int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return false;
            }

            await _employeeRepository.DeleteAsync(id);

            return true;
        }

        // Item #3
        public async Task<IEnumerable<EmployeeWithDepartment>> GetAllWithDepartmentsAsync()
        {
            var result = await _employeeRepository.GetAllWithDepartmentAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeWithProject>> GetAllWithProjectsAsync()
        {
            var result = await _employeeRepository.GetAllWithProjectsAsync();

            return result;
        }

        public async Task<IEnumerable<HighSalaryRecentEmployee>> GetHighSalaryRecentEmployeesAsync()
        {
            var result = await _employeeRepository.GetHighSalaryRecentEmployeesAsync();

            return result;
        }

        // helper function
        public async Task<bool> DepartmentExist(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);

            if (department == null)
            {
                return false;
            }

            return true;
        }
    }
}
