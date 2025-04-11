using Assignment2.Application.Extensions;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departments>> GetAllAsync()
        {
            var query = from d in _context.Departments
                        orderby d.Name
                        select new Departments
                        {
                            Id = d.Id,
                            Name = d.Name,
                        };

            var result = await query.ToListAsync();

            return result;
        }   

        public async Task<Departments?> GetByIdAsync(int id)
        {
            var query = from d in _context.Departments
                        where d.Id == id
                        select new Departments
                        {
                            Id = d.Id,
                            Name = d.Name,
                        };

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<Departments> CreateAsync(Departments department)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return department;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }
        }

        public async Task<Departments?> UpdateAsync(Departments department)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return department;
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
                var query = from d in _context.Departments
                            where d.Id == id
                            select d;

                var department = await query.FirstAsync();

                _context.Departments.Remove(department);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }
            
        }

        public async Task<Departments?> GetByNameAsync(string name)
        {
            var query = from d in _context.Departments
                        where d.Name.ToLower() == name.ToLower()
                        select new Departments
                        {
                            Id = d.Id,
                            Name = d.Name,
                        };

            var result = await query.FirstOrDefaultAsync();

            return result;
        }
    }
}
