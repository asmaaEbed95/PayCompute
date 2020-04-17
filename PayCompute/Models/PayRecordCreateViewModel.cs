using PayCompute.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace PayCompute.Models
{
    public class PayRecordCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Full Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string FullName { get; set; }

        //natioal insurance number
        public string NiNo { get; set; }

        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();

        [Display(Name ="TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        public string TaxCode { get; set; } = "1250L";

        [Display(Name ="Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; } = 144m;

        public decimal OverTimeHours { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OverTimeEarnings { get; set; }

        public decimal Tax { get; set; }

        //National Insurance Contribution
        public decimal NIC { get; set; }

        public decimal? UnionFee { get; set; }

        //student Loan Company
        //another way to make a property optional Nullable<>
        public Nullable<decimal> SLC { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
