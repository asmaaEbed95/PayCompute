using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
using PayCompute.Models;
using PayCompute.Services;

namespace PayCompute.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayComputationService _payComputationService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private readonly INationalInsuranceContributionService _nationalInsuranceContributionService;

        private decimal overTimeHours;
        private decimal contracualEarnings;
        private decimal overtimeEarnings;
        private decimal totalEarnings;
        private decimal tax;
        private decimal unionFee;
        private decimal StudentLoan;
        private decimal nationalInsurance;
        private decimal totalDeduction;

        public PayController(IPayComputationService payComputationService, IEmployeeService employeeService, ITaxService taxService, INationalInsuranceContributionService nationalInsuranceContributionService)
        {
            _payComputationService = payComputationService;
            _employeeService = employeeService;
            _taxService = taxService;
            _nationalInsuranceContributionService = nationalInsuranceContributionService;
        }

        public IActionResult Index()
        {
            //we use select to project to oue view model then we do the mapping
            var payRecords = _payComputationService.GetAll().Select(pay => new PaymentRecordIndexViewModel
            {
                Id = pay.Id,
                EmployeeId = pay.EmployeeId,
                FullName = pay.FullName,
                PayDate = pay.PayDate,
                PayMonth = pay.PayMonth,
                TaxYearId = pay.TaxYearId,
                Year = _payComputationService.GetTaxYearById(pay.TaxYearId).YearOfTax,
                TotalEarnings = pay.TotalEarnings,
                TotalDeduciton = pay.TotalDeduction,
                NetPayment = pay.NetPayment,
                Employee = pay.Employee
            });
            return View(payRecords);
        }

        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            var model = new PayRecordCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PayRecordCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetById(model.EmployeeId).FullName,
                    NiNo = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OverTimeHours = overTimeHours = _payComputationService.OvertimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contracualEarnings = _payComputationService.ContractualEarnings(model.HoursWorked, model.ContractualHours, model.HourlyRate),
                    OverTimeEarnings = overtimeEarnings = _payComputationService.OvertimeEarnings(_payComputationService.OvertimeRate(model.HourlyRate), overTimeHours),
                    TotalEarnings = totalEarnings =
                     _payComputationService.TotalEarnings(overtimeEarnings, contracualEarnings),
                    Tax = tax = _taxService.TaxAmount(totalEarnings),
                    UnionFee = unionFee =  _employeeService.UnionFees(model.EmployeeId),
                    SLC = StudentLoan = _employeeService.StudentLoanRepaymentAmoount(model.EmployeeId, totalEarnings),
                    NIC = nationalInsurance =  _nationalInsuranceContributionService.NIContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _payComputationService.TotalDeduction(tax, nationalInsurance, StudentLoan, unionFee),
                    NetPayment = _payComputationService.NetPay(totalEarnings, totalDeduction)
                };

               await _payComputationService.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();

            return View(model);
        }
    }
}