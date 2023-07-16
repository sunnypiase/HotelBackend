using Microsoft.EntityFrameworkCore;
using WebAPI.Entity;

namespace WebAPI.Repositories
{
    public class SqlServerRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly SqlHotelDbContext _context;
        private readonly DbSet<T> _entities;

        public SqlServerRepository(SqlHotelDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public IQueryable<T> GetAll() => _entities.AsNoTracking();

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> BatchAddAsync(IEnumerable<T> entityList)
        {
            await _entities.AddRangeAsync(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }
    }

}
