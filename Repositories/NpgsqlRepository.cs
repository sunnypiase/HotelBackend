﻿using Microsoft.EntityFrameworkCore;
using WebAPI.Entity;

namespace WebAPI.Repositories
{
    public class NpgsqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly NpgsqlHotelDbContext _context;
        private readonly DbSet<T> _entities;

        public NpgsqlRepository(NpgsqlHotelDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public IQueryable<T> GetAll() => _entities.AsNoTracking();

        public async Task AddAsync(T entity)
        {
            if (entity is Booking booking)
            {
                booking.Date = booking.Date.ToUniversalTime();
            }

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> BatchAddAsync(IEnumerable<T> entityList)
        {
            foreach (var entity in entityList)
            {
                if (entity is Booking booking)
                {
                    booking.Date = booking.Date.ToUniversalTime();
                }
            }

            await _entities.AddRangeAsync(entityList);
            await _context.SaveChangesAsync();
            return entityList;
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
    }
}
