using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(a => a.AccountId);

        builder.Property(a => a.AccountNumber)
               .IsRequired();
             

        builder.HasIndex(a => a.AccountNumber)
               .IsUnique();

        builder.Property(a => a.AccountType)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(a => a.AccountStatus)
               .IsRequired()
               .HasConversion<string>();
              

        builder.Property(a => a.Balance)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0m);

        builder.Property(a => a.OpenedDate)
               .IsRequired();


        builder.HasOne(a => a.Branch)
               .WithMany(b => b.Accounts)
               .HasForeignKey(a => a.BranchId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
