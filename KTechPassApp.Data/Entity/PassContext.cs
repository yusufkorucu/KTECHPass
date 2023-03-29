using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTechPassApp.Data.Entity
{
    public class PassContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        public DbSet<User> Users { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRecord> UserRecords { get; set; }

    }
}

