using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;

namespace Pushinbar.Repositories
{
    public class SnackRepository : IRepository<SnackEntity>
    {
        private readonly DBContext dbContext;
 
        public SnackRepository(string connectionString)
        {
            this.dbContext = new DBContext(connectionString);
        }
        public IEnumerable<SnackEntity> GetAll()
        {
            return dbContext.Snacks;
        }

        public async Task<SnackEntity> GetAsync(Guid id)
        {
            return await dbContext.Snacks.FindAsync(id);
        }

        public async Task CreateAsync(SnackEntity item)
        {
            await dbContext.Snacks.AddAsync(item);
        }

        public void Update(SnackEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await dbContext.Snacks.FindAsync(id);
            if (item != null)
                dbContext.Snacks.Remove(item);
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}