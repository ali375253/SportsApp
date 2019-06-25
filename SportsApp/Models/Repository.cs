using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SportsAppContext _context;
        private readonly DbSet<T> _DbSet;

        public Repository(SportsAppContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }
        public async Task<T> Add(T NewT)
        {
            await _DbSet.AddAsync(NewT);
            return NewT;
        }

        public async Task<T> Delete(int Id)
        {
            T t = await _DbSet.FindAsync(Id);
            if (t != null)
            {
                _DbSet.Remove(t);
            }
            return t;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T> Get(int Id)
        {
            return await _DbSet.FindAsync(Id);
        }

        public T Update(T UpdatedT)
        {
            var t = _DbSet.Attach(UpdatedT);
            t.State = EntityState.Modified;
            return UpdatedT;
        }

        public async Task<T> DeleteConfirmed(int id)
        {
            if (typeof(T) == typeof(Test))
            {
                _context.TestDetail.RemoveRange(_context.TestDetail.Where(x => x.TestId == id));
            }
            var t = await _DbSet.FindAsync(id);
            _DbSet.Remove(t);
            return t;
        }

    }
}
