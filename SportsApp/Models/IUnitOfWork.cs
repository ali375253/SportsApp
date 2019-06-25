using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
