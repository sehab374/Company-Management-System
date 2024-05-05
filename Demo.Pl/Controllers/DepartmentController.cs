using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.Pl.Controllers

{
    ///1st relationship : inheritance (DepartmentController is a controller)
    public class DepartmentController : Controller
    {
        ///2nd relationship : Association - composition not aggregation (must has) (DepartmentController has a IDepartmentRepository)
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo) ///ask clr for creating an object from class implementing IDepartmentRepository
        {
            _departmentRepo = departmentRepo; ///make our reference point to the obj which clr create
        }

        public IActionResult Index()
        {
            var department = _departmentRepo.GetAll();
            return View(department);
        }

        //GO TO(route to) create page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        ///will submit form in create page 
        [HttpPost]
        public IActionResult Create(Department department)
        {
            //check if department validate the restriction i make in department class  
            //server side validation
            if (ModelState.IsValid)
            {
                //access database
                var count=_departmentRepo.Add(department);
                if(count > 0)
                    return RedirectToAction("Index");
            }
            return View(department);
        }

        //[HttpGet]
        //public IActionResult Details(int? id)
        //{
        //    if (!id.HasValue)
        //        return BadRequest();
        //    var department= _departmentRepo.Get(id.Value);
        //    if(department is null)
        //        return NotFound();
        //    return View(department);
        //}

        [HttpGet]
        public IActionResult Details(int? id,string viewName= "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentRepo.Get(id.Value);
            if (department is null)
                return NotFound();
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

            #region with Dublicate
            //if (!id.HasValue)
            //    return BadRequest();
            //var department = _departmentRepo.Get(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department); 
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id!=department.Id)
                return BadRequest();    
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
                {
                    _departmentRepo.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(department);

            }
        }




        //1
        //return View();
        //2 (department) => model which will send 
        //return View(department);
        //3 ("otherview") => name of view which name different of action name
        //return View("otherview");
        //4 ("otherview") => name of view which name different of action name && sent to it  (department) => model which will send 
        //return View("otherview",department);
    }
}
