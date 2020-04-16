using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PayCompute.Entity
{
    public class PaymentRecord
    {
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        //natioal insurance number
        public string NiNo { get; set; }

        public DateTime PayDate { get; set; }

        public string PayMonth { get; set; }

        [ForeignKey("TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        public string TaxCode { get; set; }

        [Column(TypeName = "money")]
        public decimal HourlyRate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal HoursWorked { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContractualHours { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverTimeHours { get; set; }

        [Column(TypeName = "money")]
        public decimal ContractualEarnings { get; set; }

        [Column(TypeName = "money")]
        public decimal OverTimeEarnings { get; set; }

        [Column(TypeName = "money")]
        public decimal Tax { get; set; }

        //National Insurance Contribution
        [Column(TypeName = "money")]
        public decimal NIC { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnionFee { get; set; }

        //student Loan Company
        //another way to make a property optional Nullable<>
        [Column(TypeName = "money")]
        public Nullable<decimal> SLC { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalEarnings { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalDeduction { get; set; }

        [Column(TypeName ="money")]
        public decimal NetPayment { get; set; }
    }
}
