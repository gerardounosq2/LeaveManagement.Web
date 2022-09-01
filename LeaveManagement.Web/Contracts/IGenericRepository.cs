namespace LeaveManagement.Web.Contracts
{
   public interface IGenericRepository<T> where T : class
   {
      Task<T> GetByIdAsync(int? id);
      Task<IEnumerable<T>> GetAllAsync();
      Task<T> AddAsync(T entity);
      Task AddRangeAsync(IEnumerable<T> entities);
      Task DeleteAsync(int id);
      Task UpdateAsync(T entity);
      Task<bool>Exists(int id);
   }
}