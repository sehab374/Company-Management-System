using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;
    
      public  DepartmentRepository(AppDbContext dbContext)
        //(AppDbContext dbContext)=>when he try to make obj from AppDbContext find it created in the past in StartUp 
        //in constructor it will be initailize _dbContext with the value of dbContext
        {
            _dbContext =dbContext;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity); //only change obj state from unchanged to Added
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity); //only change obj state from unchanged to modified
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity); //only change obj state from unchanged to deleted
            return _dbContext.SaveChanges();
        }
        public Department Get(int id)
        {
            ///var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            ///if(department is null)
            ///    department=_dbContext.Departments.Where(D=>D.Id==id).FirstOrDefault();
            ///return department;

            //return _dbContext.Departments.Find(id);
            return _dbContext.Find<Department>(id);
        }

        public IEnumerable<Department> GetAll()
            => _dbContext.Departments.AsNoTracking().ToList();
        

      
    }
}
