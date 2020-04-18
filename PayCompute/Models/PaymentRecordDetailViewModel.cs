using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute.Models
{
    public class PaymentRecordDetailViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        //natioal insurance number
        public string NiNo { get; set; }

        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; }

        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; }

        [Display(Name ="Tax Year")]
        public int TaxYearId { get; set; }

        public string Year { get; set; }

        public TaxYear TaxYear { get; set; }

        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; }

        [Display(Name ="Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; }

        [Display(Name = "Overtime Hours")]
        public decimal OverTimeHours { get; set; }

        [Display(Name = "Overtime Rate")]
        public decimal OverTimeRate { get; set; }

        [Display(Name = "Contractual Earnings")]
        public decimal ContractualEarnings { get; set; }

        [Display(Name = "Overtime Earnings")]
        public decimal OverTimeEarnings { get; set; }

        public decimal Tax { get; set; }

        //National Insurance Contribution
        public decimal NIC { get; set; }

        [Display(Name = "Union Fee")]
        public decimal? UnionFee { get; set; }

        //student Loan Company
        //another way to make a property optional Nullable<>
        public Nullable<decimal> SLC { get; set; }

        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }

        [Display(Name = "Total Deduction")]
        public decimal TotalDeduction { get; set; }

        [Display(Name = "Net Payment")]
        public decimal NetPayment { get; set; }
    }
}
