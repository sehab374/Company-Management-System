using Demo.DAL.Data.Configrations;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {

        }
       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       ////"Server=.;DataBase= MVCApplication;Trusted_Connection=True;MultipleActiveResultSets=True"
       //=> optionsBuilder.UseSqlServer("Server=.;DataBase= MVCApplication;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call configration classes
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigrations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; }
    }
}
