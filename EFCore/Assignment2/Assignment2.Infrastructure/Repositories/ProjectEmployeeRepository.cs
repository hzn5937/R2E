using Assignment2.Application.Extensions;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployeeRepository
    {
        private readonly AppDbContext _context;

        public ProjectEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEmployees>> GetAllAsync(int projectId)
        {
            var query = from pe in _context.ProjectEmployees
                        where pe.ProjectId == projectId
                        orderby pe.Employee.Name
                        select new ProjectEmployees
                        {
                            ProjectId = pe.ProjectId,
                            EmployeeId = pe.EmployeeId,
                            Enable = pe.Enable,
                            Employee = pe.Employee,
                            Project = pe.Project,
                        };

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<ProjectEmployees?> GetAsync(int projectId, int employeeId)
        {
            var query = from pe in _context.ProjectEmployees
                        where pe.ProjectId == projectId && pe.EmployeeId == employeeId
                        select new ProjectEmployees
                        {
                            ProjectId = pe.ProjectId,
                            EmployeeId = pe.EmployeeId,
                            Enable = pe.Enable,
                            Employee = pe.Employee,
                            Project = pe.Project,
                        };

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<ProjectEmployees> CreateAsync(ProjectEmployees entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.ProjectEmployees.Add(entity);
                await _context.SaveChangesAsync();

                await _context.Entry(entity).Reference(pe => pe.Employee).LoadAsync();
                await _context.Entry(entity).Reference(pe => pe.Project).LoadAsync();

                await transaction.CommitAsync();
                return entity;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("Transaction failed");
            }

            
        }

        public async Task<bool> UpdateAsync(ProjectEmployees entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.ProjectEmployees.Update(entity);
                await _context.SaveChangesAsync();
                await _context.Entry(entity).Reference(pe => pe.Employee).LoadAsync();
                await _context.Entry(entity).Reference(pe => pe.Project).LoadAsync();

                await transaction.CommitAsync();
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("Transaction failed");
            }

        }

        public async Task<bool> DeleteAsync(ProjectEmployees entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.ProjectEmployees.Remove(entity);
                var output = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return output > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("Transaction failed");
            }
        }

        public async Task<bool> ExistsAsync(int projectId, int employeeId)
        {
            return await _context.ProjectEmployees
                .AnyAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
        }
    }
}
