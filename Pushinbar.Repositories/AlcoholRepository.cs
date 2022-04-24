using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;

namespace Pushinbar.Repositories
{
    public class AlcoholRepository : IRepository<AlcoholEntity>
    {
        private readonly DBContext dbContext;
 
        public AlcoholRepository(string connectionString)
        {
            this.dbContext = new DBContext(connectionString);
        }
        
        public IEnumerable<AlcoholEntity> GetAll()
        {
            return dbContext.Alcohol;
        }

        public async Task<AlcoholEntity> GetAsync(Guid id)
        {
            return await dbContext.Alcohol.FindAsync(id);
        }

        public async Task CreateAsync(AlcoholEntity item)
        {
            await dbContext.Alcohol.AddAsync(item);
        }

        public void Update(AlcoholEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await dbContext.Alcohol.FindAsync();
            if (item != null)
                dbContext.Alcohol.Remove(item);
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