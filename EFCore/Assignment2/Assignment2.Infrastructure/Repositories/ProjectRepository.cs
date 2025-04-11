using Assignment2.Application.Extensions;
using Assignment2.Domain.Entities;
using Assignment2.Domain.Interfaces;
using Assignment2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Projects?> GetByIdAsync(int id)
        {
            var query = from p in _context.Projects
                        where p.Id == id
                        select new Projects
                        {
                            Id = p.Id,
                            Name = p.Name,
                        };

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<Projects>> GetAllAsync()
        {
            var query = from p in _context.Projects
                        orderby p.Name
                        select new Projects
                        {
                            Id = p.Id,
                            Name = p.Name,
                        };

            var result = await query.ToListAsync();

            return result;
        }


        public async Task<Projects> CreateAsync(Projects project)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return project;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }

            
        }

        public async Task<Projects?> UpdateAsync(Projects project)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingProject = await GetByIdAsync(project.Id);
                if (existingProject == null)
                {
                    throw new NotFoundException("Project not found");
                }

                existingProject.Name = project.Name;

                _context.Projects.Update(existingProject);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return existingProject;
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
                var query = from p in _context.Projects
                            where p.Id == id
                            select p;

                var project = await query.FirstAsync();

                _context.Projects.Remove(project);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException();
            }
        }

        public async Task<Projects?> GetByNameAsync(string name)
        {
            var query = from p in _context.Projects
                        where p.Name == name
                        select p;

            var result = await query.FirstOrDefaultAsync();

            return result;
        }
    }
}
