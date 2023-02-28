using Demo.Data.DB.SqlServer;
using Demo.Data.Repositories.Interfaces;
using Demo.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Demo.Data.Repositories.Implementations
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public Repository()
        {
        }

        public T Add(T item)
        {
            DbSet<T> dbSet = _context.Set<T>();
            if (dbSet == default(DbSet<T>))
                return default(T);
            T result = dbSet.Add(item).Entity;
            _context.SaveChanges();
            return result;
        }

        public List<T> AddAll(IEnumerable<T> items)
        {
            List<T> result = new List<T>();

            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(List<T>);

            foreach (T item in items)
            {
                T entity = dbSet.Add(item).Entity;
                result.Add(entity);
            }

            _context.SaveChanges();
            return result;
        }

        public void Delete(T item)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return;

            dbSet.Remove(item);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(List<T>);

            return dbSet.ToList();
        }

        public T GetById(uint id)
        {
            DbSet<T> dbSet = _context.Set<T>();

            if (dbSet == default(DbSet<T>))
                return default(T);

            T item = dbSet
                .FirstOrDefault(obj => obj.Id == id);

            return item;
        }

        public void Edit(T item)
        {
            // проверяем, что сущность не null
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // прикрепляем сущность к контексту данных
            _context.Set<T>().Attach(item);

            // устанавливаем состояние модели на изменено
            _context.Entry(item).State = EntityState.Modified;

            // сохраняем изменения в базе данных
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
