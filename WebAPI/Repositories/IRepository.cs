using WebAPI.Entity;

namespace WebAPI.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task<IEnumerable<T>> BatchAddAsync(IEnumerable<T> entityList);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Query();
    }
}
