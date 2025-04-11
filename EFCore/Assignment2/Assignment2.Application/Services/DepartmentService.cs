using Assignment2.Application.DTOs.Departments;
using Assignment2.Application.Extensions;
using Assignment2.Application.Interfaces;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using AutoMapper;

namespace Assignment2.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentOutputDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();

            var output = _mapper.Map<IEnumerable<DepartmentOutputDto>>(departments);

            return output;
        }

        public async Task<DepartmentOutputDto?> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return null;
            }

            var output = _mapper.Map<DepartmentOutputDto?>(department);

            return output;
        }

        public async Task<DepartmentOutputDto?> CreateAsync(CreateDepartmentDto departmentDto)
        {
            if (await IsDuplicate(departmentDto.Name))
            {
                throw new ConflictException($"A department with the name of '{departmentDto.Name}' already exists");
            }

            var department = _mapper.Map<Departments>(departmentDto);

            var createdDepartment = await _departmentRepository.CreateAsync(department);

            var output = _mapper.Map<DepartmentOutputDto>(createdDepartment);

            return output;
        }

        public async Task<DepartmentOutputDto?> UpdateAsync(int id, UpdateDepartmentDto departmentDto)
        {
            if (await IsDuplicate(departmentDto.Name))
            {
                throw new ConflictException($"A department with the name of '{departmentDto.Name}' already exists");
            }

            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return null;
            }

            _mapper.Map(departmentDto, department);

            var updatedDepartment = await _departmentRepository.UpdateAsync(department);

            var output = _mapper.Map<DepartmentOutputDto>(updatedDepartment);

            return output;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return false;
            }

            await _departmentRepository.DeleteAsync(id);
            
            return true;
        }

        // helper functions
        public async Task<bool> IsDuplicate(string name)
        {
            var department = await _departmentRepository.GetByNameAsync(name);

            if (department == null)
            {
                return false;
            }

            return true;
        }
    }
}
