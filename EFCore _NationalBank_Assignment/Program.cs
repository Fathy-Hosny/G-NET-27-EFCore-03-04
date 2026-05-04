using System.Globalization;
using BankManagement.Entities;
using EFCore__NationalBank_Assignment.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCore__NationalBank_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new NationalBankDbContext();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("        National Bank      ");
                Console.WriteLine("========================================");
                Console.WriteLine("  1) Add a new Customer");
                Console.WriteLine("  2) Add a new Branch");
                Console.WriteLine("  3) Open a new Account (Select Branch)");
                Console.WriteLine("  4) Update Account Status (Active/Closed)");
                Console.WriteLine("  5) Remove Account from Customer");
                Console.WriteLine("  6) List All Data (Report)");
                Console.WriteLine("  0) Exit");
                Console.WriteLine("----------------------------------------");
                Console.Write("  Enter choice: ");

                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice)) { Pause(); continue; }

                switch (choice)
                {
                    case 1: AddCustomer(db); break;
                    case 2: AddBranch(db); break;
                    case 3: OpenAccount(db); break;
                    case 4: UpdateAccountStatus(db); break;
                    case 5: RemoveAccountFromCustomer(db); break;
                    case 6: ListCustomers(db); break;
                    case 0: return;
                    default: Console.WriteLine("Invalid Option."); break;
                }
                Pause();
            }
        }

        static void AddCustomer(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- Add New Customer ---");
            string fullName = ReadRequired("Full Name");
            string nationalId = ReadRequired("National ID");
            DateTime dob = ReadDate("Date of Birth");
            string maritalStatus = ReadRequired("Marital Status");
            string email = ReadRequired("Email");
            string phone = ReadRequired("Phone");
            string address = ReadRequired("Address");

            Console.WriteLine("Type: 1) Individual | 2) Business");
            string? type = Console.ReadLine();

            try
            {
                if (type == "1")
                {
                    string occupation = ReadRequired("Occupation");
                    db.IndividualCustomers.Add(new IndividualCustomer
                    {
                        FullName = fullName,
                        NationalId = nationalId,
                        DateOfBirth = dob,
                        MaritalStatus = maritalStatus,
                        Email = email,
                        Phone = phone,
                        Address = address,
                        Occupation = occupation,
                        CustomerType = CustomerType.Individual
                    
                       
                    });

                }
                else
                {
                    string businessName = ReadRequired("Company Name");
                    db.BusinessCustomers.Add(new BusinessCustomer
                    {
                        FullName = fullName,
                        NationalId = nationalId,
                        DateOfBirth = dob,
                        Email = email,
                        Phone = phone,
                        CustomerType = CustomerType.Business,
                        Address = address,
                        CompanyName = businessName
                    });
                }
                db.SaveChanges();
                Console.WriteLine("\n Customer added successfully.");
            }
            catch (Exception ex) { Console.WriteLine($" Error: {ex.Message}"); }
        }

        static void AddBranch(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- Add New Branch ---");
            string code = ReadRequired("Branch Code (e.g. B01)");
            string name = ReadRequired("Branch Name");
            string city = ReadRequired("City");
            string address = ReadRequired("Address");
            string phone = ReadRequired("Phone");

            try
            {
                db.Branches.Add(new Branch
                {
                    BranchCode = code,
                    BranchName = name,
                    City = city,
                    Address = address,
                    Phone = phone
                });
                db.SaveChanges();
                Console.WriteLine("\nBranch added!");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        }

        static void OpenAccount(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- Open New Account ---");

            var branches = db.Branches.ToList();
            if (!branches.Any())
            {
                Console.WriteLine("[!] No branches found. Please add a branch first (Option 2).");
                return;
            }

            Console.WriteLine("Select a Branch:");
            for (int i = 0; i < branches.Count; i++)
            {
                Console.WriteLine($"  {i + 1}) {branches[i].BranchName} [{branches[i].BranchCode}]");
            }

            int branchSelection = ReadInt("Enter Branch Number");
            if (branchSelection < 1 || branchSelection > branches.Count) return;

            string selectedBranchCode = branches[branchSelection - 1].BranchCode;

            int custId = ReadInt("Customer Id");
            var customer = db.IndividualCustomers.Find(custId) ?? (Customer?)db.BusinessCustomers.Find(custId);
            if (customer == null) { Console.WriteLine("[!] Customer not found!"); return; }

            string accNum = ReadRequired("Account Number");
            decimal initialBalance = ReadDecimal("Initial Balance");

            try
            {
                var account = new Account
                {
                    AccountNumber = accNum,
                    Balance = initialBalance,
                    AccountStatus = AccountStatus.Active,
                    OpenedDate = DateTime.Now,
                    BranchCode = selectedBranchCode
                };
                db.Accounts.Add(account);
                db.CustomerAccounts.Add(new CustomerAccount { Customer = customer, Account = account, OwnershipRole = OwnershipRole.Primary });
                db.SaveChanges();
                Console.WriteLine($"\nAccount created in '{branches[branchSelection - 1].BranchName}' successfully.");
            }
            catch (Exception ex) { Console.WriteLine($" Error: {ex.Message}"); }
        }

        static void UpdateAccountStatus(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- Update Account Status ---");
            string accNum = ReadRequired("Enter Account Number");
            var account = db.Accounts.FirstOrDefault(a => a.AccountNumber == accNum);

            if (account == null) { Console.WriteLine("[!] Account not found."); return; }

            Console.WriteLine($"Current Status: {account.AccountStatus}");
            Console.WriteLine("Select New Status: 1) Active | 2) Closed");
            account.AccountStatus = (Console.ReadLine() == "2") ? AccountStatus.Closed : AccountStatus.Active;

            db.SaveChanges();
            Console.WriteLine($"Status updated to {account.AccountStatus}.");
        }

        static void RemoveAccountFromCustomer(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- Remove Account From Customer ---");
            int custId = ReadInt("Customer Id");
            string accNum = ReadRequired("Account Number");

            var link = db.CustomerAccounts.Include(ca => ca.Account)
                         .FirstOrDefault(ca => ca.CustomerId == custId && ca.Account.AccountNumber == accNum);

            if (link == null) { Console.WriteLine("[!] Link not found."); return; }

            db.CustomerAccounts.Remove(link);
            db.SaveChanges();
            Console.WriteLine("Account unlinked from customer.");
        }

        static void ListCustomers(NationalBankDbContext db)
        {
            Console.WriteLine("\n--- System Report ---\n");
            var indv = db.IndividualCustomers.Include(c => c.CustomerAccounts).ThenInclude(ca => ca.Account).ToList();
            var biz = db.BusinessCustomers.Include(c => c.CustomerAccounts).ThenInclude(ca => ca.Account).ToList();
            var all = indv.Cast<Customer>().Concat(biz);

            foreach (var c in all)
            {
                Console.WriteLine($"Customer: {c.FullName} (ID: {c.CustomerId})");
                foreach (var ca in c.CustomerAccounts)
                    Console.WriteLine($"   --> Acc: {ca.Account.AccountNumber} | Bal: {ca.Account.Balance:N2} | Branch: {ca.Account.BranchCode}");
                Console.WriteLine("--------------------------------------------");
            }
        }

        static void Pause() { Console.WriteLine("\nPress any key..."); Console.ReadKey(); }
        static string ReadRequired(string label)
        {
            Console.Write($"{label}: "); string? s = Console.ReadLine();
            return string.IsNullOrWhiteSpace(s) ? "N/A" : s.Trim();
        }
        static int ReadInt(string label)
        {
            Console.Write($"{label}: "); int.TryParse(Console.ReadLine(), out int i); return i;
        }
        static decimal ReadDecimal(string label)
        {
            Console.Write($"{label}: "); decimal.TryParse(Console.ReadLine(), out decimal d); return d;
        }
        static DateTime ReadDate(string label)
        {
            Console.Write($"{label} (yyyy/mm/dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime d); return d;
        }
    }
}