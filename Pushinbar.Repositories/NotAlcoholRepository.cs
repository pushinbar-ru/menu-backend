using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models.NotAlcohol;

namespace Pushinbar.Repositories
{
    public class NotAlcoholRepository : IRepository<NotAlcoholEntity>
    {
        private readonly DBContext dbContext;
 
        public NotAlcoholRepository(string connectionString)
        {
            this.dbContext = new DBContext(connectionString);
        }

        public IEnumerable<NotAlcoholEntity> GetAll()
        {
            return dbContext.NotAlcohol;
        }

        public async Task<NotAlcoholEntity?> GetAsync(Guid id)
        {
            throw await dbContext.NotAlcohol.FindAsync(id);
        }

        public async Task CreateAsync(NotAlcoholEntity item)
        {
            await dbContext.NotAlcohol.AddAsync(item);
        }

        public void Update(NotAlcoholEntity item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await dbContext.NotAlcohol.FindAsync(id);
            if (item != null)
                dbContext.NotAlcohol.Remove(item);
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