using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using PayCompute.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private decimal studentLoanAmount;

        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Employee newEmployee)
        {
           await _context.Employees.AddAsync(newEmployee);
           await _context.SaveChangesAsync();
        }

        public Employee GetById(int employeeId) => _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();

        public async Task Delete(int employeeId)
        {
            var employee = GetById(employeeId);
           _context.Remove(employee);
           await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll() => _context.Employees;

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public decimal StudentLoanRepaymentAmoount(int id, decimal totalAmount)
        {
            //we get student load repayment from www.studentloanrepayment.co.uk
            var employee = GetById(id);

            if (employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanAmount = 60m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanAmount = 83m;
            }
            else
            {
                studentLoanAmount = 0m;
            }

            return studentLoanAmount;
        }

        public decimal UnionFees(int id)
        {
            var employee = GetById(id);

            var fee = (employee.UnionMember == UnionMember.Yes) ? 10m : 0m;

            return fee;
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayroll()
        {
            return GetAll().Select(employee => new SelectListItem()
            {
                Text = employee.FullName,
                Value = employee.Id.ToString()
            });
        }
    }
}
