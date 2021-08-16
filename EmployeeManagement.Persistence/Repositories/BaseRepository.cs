using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly EmployeeManagementDBContext context;

        public BaseRepository(EmployeeManagementDBContext dbContext)
        {
            context = dbContext; 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
             await context.Set<T>().AddAsync(entity);
             await context.SaveChangesAsync();
             return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
