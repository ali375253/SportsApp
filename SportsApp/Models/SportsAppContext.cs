using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsApp.Models;

namespace SportsApp.Models
{
    public class SportsAppContext : DbContext
    {
        public SportsAppContext(DbContextOptions<SportsAppContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestDetail> TestDetail { get; set; }
        public DbSet<SportsApp.Models.TestList> TestList { get; set; }
    }
}
