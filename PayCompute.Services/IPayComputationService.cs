﻿using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services
{
    public interface IPayComputationService
    {
        Task CreateAsync(PaymentRecord paymentRecord);

        PaymentRecord GetById(int id);

        TaxYear GetTaxYearById(int id);

        IEnumerable<PaymentRecord> GetAll();

        IEnumerable<SelectListItem> GetAllTaxYear();

        decimal OvertimeHours(decimal hoursWorked, decimal contractualHours);

        decimal ContractualEarnings(decimal hoursWorked, decimal contractualHours, decimal hourlyRate);

        decimal OvertimeRate(decimal hourlyRate);

        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);

        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);

        decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanPayment, decimal unionFees);

        decimal NetPay(decimal totalEarnings, decimal totalDeduction);
    }
}
