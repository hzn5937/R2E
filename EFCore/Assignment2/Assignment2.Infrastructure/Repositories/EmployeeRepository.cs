using Assignment2.Application.Extensions;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Domain.Models;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employees> CreateAsync(Employees employee)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                await _context.Entry(employee).Reference(e => e.Department).LoadAsync();

                await transaction.CommitAsync();
                return employee;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            var query = from e in _context.Employees
                        orderby e.Name
                        select new Employees
                        {
                            Id = e.Id,
                            Name = e.Name,
                            DepartmentId = e.DepartmentId,
                            JoinedDate = e.JoinedDate,
                            Department = e.Department,
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<Employees?> GetByIdAsync(int id)
        {
            var query = from e in _context.Employees
                        where e.Id == id
                        select new Employees
                        {
                            Id = e.Id,
                            Name = e.Name,
                            DepartmentId = e.DepartmentId,
                            JoinedDate = e.JoinedDate,
                            Department = e.Department,
                        };

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<Employees?> UpdateAsync(Employees employee)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existing = await _context.Employees.FindAsync(employee.Id);

                if (existing == null)
                {
                    return null;
                }
                existing.Name = employee.Name;
                existing.DepartmentId = employee.DepartmentId;
                existing.JoinedDate = employee.JoinedDate;

                await _context.SaveChangesAsync();

                await _context.Entry(employee).Reference(e => e.Department).LoadAsync();

                await transaction.CommitAsync();
                return employee;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }

        public async Task DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var employee = await GetByIdAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }

        // Item #3
        public async Task<IEnumerable<EmployeeWithDepartment>> GetAllWithDepartmentAsync()
        {
            string sql = @"SELECT e.Id AS EmployeeId, e.Name AS EmployeeName, d.Name AS DepartmentName
                            FROM Employees e
                            INNER JOIN Departments d ON e.DepartmentId = d.Id";

            var result = await _context.Database.SqlQueryRaw<EmployeeWithDepartment>(sql).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<EmployeeWithProject>> GetAllWithProjectsAsync()
        {
            string sql = @"SELECT e.Id AS EmployeeId, e.Name AS EmployeeName, p.Name AS ProjectName
                          FROM Employees e
                          LEFT JOIN Project_Employee pe ON e.Id = pe.EmployeeId
                          LEFT JOIN Projects p ON pe.ProjectId = p.Id";

            var result = await _context.Database.SqlQueryRaw<EmployeeWithProject>(sql).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<HighSalaryRecentEmployee>> GetHighSalaryRecentEmployeesAsync()
        {
            string sql = @"SELECT e.Id AS EmployeeId, e.Name AS EmployeeName, s.Salary AS Salary, e.JoinedDate
                          FROM Employees e
                          INNER JOIN Salaries s ON e.Id = s.EmployeeId
                          WHERE s.Salary > 100 AND e.JoinedDate >= '2024-01-01'";

            var result = await _context.Database.SqlQueryRaw<HighSalaryRecentEmployee>(sql).ToListAsync();

            return result;
        }
    }
}
