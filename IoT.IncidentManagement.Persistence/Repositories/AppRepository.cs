using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class AppRepository<T> : IAppRepository<T> where T : class
    {

        protected readonly IncidentManagementDbContext dbContext;

        public AppRepository(IncidentManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var fromDb = await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return fromDb.Entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().ToListAsync();

        public virtual async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FindAsync(id);

        public virtual async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
