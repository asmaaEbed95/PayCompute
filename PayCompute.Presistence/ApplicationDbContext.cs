﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayCompute.Entity;

namespace PayCompute.Presistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TaxYear> TaxYears { get; set; }
    }
}
