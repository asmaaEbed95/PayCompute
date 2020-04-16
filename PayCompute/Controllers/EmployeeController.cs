using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
using PayCompute.Models;
using PayCompute.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        private readonly HostingEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employeeService, HostingEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                ImageUrl = employee.ImageUrl,
                City = employee.City,
                Gender = employee.Gender,
                Designation = employee.Designation,
                DateJoined = employee.DateJoined
            }).ToList();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //prevent cross-site request forgery attacks
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    City = model.City,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    Phone = model.Phone,
                    PostCode = model.Postcode,
                    MiddleName = model.MiddleName,
                    Designation = model.Designation
                };

                if(model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDirectory = @"images/employee";

                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);

                    var extension = Path.GetExtension(model.ImageUrl.FileName);

                    var webRootPath = _hostingEnvironment.WebRootPath;

              fileName = DateTime.UtcNow.ToString("yymmssff") + fileName + extension;

                    var path = Path.Combine(webRootPath, uploadDirectory, fileName);

                await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

                    employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
                }

                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                City = employee.City,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                Phone = employee.Phone,
                Postcode = employee.PostCode,
                MiddleName = employee.MiddleName,
                Designation = employee.Designation
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);

                if(employee == null)
                {
                    return NotFound();
                }

                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.MiddleName = model.MiddleName;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DOB = model.DOB;
                employee.DateJoined = model.DateJoined;
                employee.Phone = model.Phone;
                employee.Designation = model.Designation;
                employee.PaymentMethod = model.PaymentMethod;
                employee.StudentLoan = model.StudentLoan;
                employee.UnionMember = model.UnionMember;
                employee.PostCode = model.Postcode;
                employee.Address = model.Address;
                employee.City = model.City;
                
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    ImageSetup(model);
                }

                await _employeeService.UpdateAsync(employee);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _employeeService.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            EmployeeDetailViewModel model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                Email = employee.Email,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                Phone = employee.Phone,
                PaymentMethod = employee.PaymentMethod,
                UnionMember = employee.UnionMember,
                StudentLoan = employee.StudentLoan,
                Address = employee.Address,
                City = employee.City,
                ImageUrl = employee.ImageUrl,
                PostCode = employee.PostCode
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
           await _employeeService.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }

        public async void ImageSetup(EmployeeEditViewModel model)
        {
            var employee = _employeeService.GetById(model.Id);

            var uploadDirectory = @"images/employee";

            var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);

            var extension = Path.GetExtension(model.ImageUrl.FileName);

            var webRootPath = _hostingEnvironment.WebRootPath;

            fileName = DateTime.UtcNow.ToString("yymmssff") + fileName + extension;

            var path = Path.Combine(webRootPath, uploadDirectory, fileName);

           await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));

            employee.ImageUrl = "/" + uploadDirectory + "/" + fileName;
        }
    }
}