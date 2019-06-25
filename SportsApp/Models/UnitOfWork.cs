using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportsAppContext _context;

        public UnitOfWork(SportsAppContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
