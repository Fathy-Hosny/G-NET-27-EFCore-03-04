using System;
using System.Collections.Generic;
using System.Text;
using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore__NationalBank_Assignment.Data
{
    internal class NationalBankDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            optionsBuilder.UseSqlServer( @"Server=.;Database=NationalBankDB;Trusted_Connection=True; TrustServerCertificate=True");

        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<BusinessCustomer> BusinessCustomers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
