using EmployeeMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EmployeeMgmt.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmployeeRepository employeeRepository, 
                            IHostingEnvironment hostingEnvironment,
                            ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee newEmp = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = ProcessUploadFile(model)
                };
                _employeeRepository.Add(newEmp);
                return RedirectToAction("Details", "Home", new { id = newEmp.Id });
            }
            return View();
        }

        public ViewResult Details(int? id)
        {

            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");

            Employee empDetail = _employeeRepository.GetEmployee(id.Value);
            if(empDetail == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel model = new HomeDetailsViewModel()
            {
                Employee = empDetail,
            };
            return View(model);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee emp = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Department = emp.Department,
                ExistingPhotoPath = emp.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                Employee updateEmp = _employeeRepository.GetEmployee(model.Id);
                updateEmp.Name = model.Name;
                updateEmp.Email = model.Email;
                updateEmp.Department = model.Department;
                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    updateEmp.PhotoPath = ProcessUploadFile(model);
                }
                _employeeRepository.Update(updateEmp);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            Employee delEmp = _employeeRepository.GetEmployee(id);
            _employeeRepository.Delete(delEmp.Id);
            if (delEmp.PhotoPath != null)
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", delEmp.PhotoPath);
                System.IO.File.Delete(filePath);
            }
            return RedirectToAction("index");
        }

        private string ProcessUploadFile(CreateEmployeeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFiles = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFiles, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
