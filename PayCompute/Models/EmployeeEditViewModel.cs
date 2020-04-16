using Microsoft.AspNetCore.Http;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayCompute.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        //regular means start with capital min 3 characters and max 3 characters and numbers from 1 to 9 
        [Required(ErrorMessage = "Employee Number is Required"), RegularExpression(@"^[A-Z]{3,3}[0-9]$")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "First Name is Required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*&"), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is Required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*&"), Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }

        [DataType(DataType.Date), Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date), Display(Name = "DateJoined")]
        public DateTime DateJoined { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Job Role is Required"), StringLength(100)]
        public string Designation { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(50), Display(Name = "National Insurance No.")]
        //example 00-000-0000 @"^\d{2}-\d{3}-\d{4}$" ... \d means anything
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
        public string NationalInsuranceNo { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }

        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }

        [Required, StringLength(150)]
        public string Address { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(50)]
        public string Postcode { get; set; }
    }
}
