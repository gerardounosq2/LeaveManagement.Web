using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
   public class GenericRepository<T> : IGenericRepository<T> where T : class
   {
      private readonly ApplicationDbContext context;

      public GenericRepository(ApplicationDbContext context)
      {
         this.context = context;
      }

      public async Task<T> AddAsync(T entity)
      {
         await context.AddAsync(entity);
         await context.SaveChangesAsync();
         return entity;
      }

      public async Task DeleteAsync(int id)
      {
         var entity = await GetByIdAsync(id);
         context.Remove(entity);
         await context.SaveChangesAsync();
      }

      public async Task<bool> Exists(int id) => await GetByIdAsync(id) != null;

      public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

      public async Task<T> GetByIdAsync(int? id)
      {
         if (id == null)
            return null;
         return await context.Set<T>().FindAsync(id);
      }

      public async Task UpdateAsync(T entity)
      {
         context.Entry(entity).State = EntityState.Modified;
         await context.SaveChangesAsync();
      }
   }
}
