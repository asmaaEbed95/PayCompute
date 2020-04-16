using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Models;
using PayCompute.Services;

namespace PayCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService )
        {
            _employeeService = employeeService;
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
    }
}