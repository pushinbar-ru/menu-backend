using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;

namespace Pushinbar.Repositories
{
    public class EatRepository : IRepository<EatEntity>
    {
        private readonly DBContext dbContext;
 
        public EatRepository(string connectionString)
        {
            this.dbContext = new DBContext(connectionString);
        }
        
        public IEnumerable<EatEntity> GetAll()
        {
            return dbContext.Eat;
        }

        public async Task<EatEntity> GetAsync(Guid id)
        {
            return await dbContext.Eat.FindAsync(id);
        }

        public async Task CreateAsync(EatEntity item)
        {
            await dbContext.Eat.AddAsync(item);
        }

        public void Update(EatEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await dbContext.Eat.FindAsync(id);
            if (item != null)
                dbContext.Eat.Remove(item);
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