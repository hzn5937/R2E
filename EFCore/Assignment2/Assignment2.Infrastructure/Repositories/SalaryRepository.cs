using Assignment2.Application.Extensions;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly AppDbContext _context;

        public SalaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salaries>> GetAllAsync()
        {
            var query = from s in _context.Salaries
                        orderby s.Employee.Name
                        select new Salaries
                        {
                            Id = s.Id,
                            Salary = s.Salary,
                            EmployeeId = s.EmployeeId,
                            Employee = s.Employee,
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<Salaries?> GetByIdAsync(int id)
        {
            var query = from s in _context.Salaries
                        where s.Id == id
                        select new Salaries
                        {
                            Id = s.Id,
                            Salary = s.Salary,
                            EmployeeId = s.EmployeeId,
                            Employee = s.Employee,
                        };

            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<Salaries> CreateAsync(Salaries salary)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingEmployeeIdQuery = from s in _context.Salaries
                                              where s.EmployeeId == salary.EmployeeId
                                              select s;

                if (await existingEmployeeIdQuery.AnyAsync())
                {
                    throw new ConflictException($"Employee with ID {salary.EmployeeId} already has a salary, consider using 'update'.");
                }


                await _context.Salaries.AddAsync(salary);
                await _context.SaveChangesAsync();
                await _context.Entry(salary).Reference(s => s.Employee).LoadAsync();

                await transaction.CommitAsync();
                return salary;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }

        public async Task<Salaries?> UpdateAsync(Salaries salary)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingSalaryQuery = from s in _context.Salaries
                                          where s.EmployeeId == salary.EmployeeId
                                          select s;

                var existingSalary = await existingSalaryQuery.FirstAsync();

                existingSalary.Salary = salary.Salary;
                existingSalary.EmployeeId = salary.EmployeeId;
                _context.Salaries.Update(existingSalary);
                await _context.SaveChangesAsync();

                await _context.Entry(existingSalary).Reference(s => s.Employee).LoadAsync();

                await transaction.CommitAsync();
                return existingSalary;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }
        }

        public async Task DeleteAsync(int employeeId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var query = from s in _context.Salaries
                            where s.EmployeeId == employeeId
                            select s;

                var salary = await query.FirstOrDefaultAsync();

                if (salary == null)
                {
                    throw new NotFoundException($"Salary with Employee ID {employeeId} not found.");
                }

                _context.Salaries.Remove(salary);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }
    }
}
