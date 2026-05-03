using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
{
    public void Configure(EntityTypeBuilder<CustomerAccount> builder)
    {
        builder.ToTable("CustomerAccounts");


        builder.HasKey(ca => new { ca.CustomerId, ca.AccountId });

        builder.Property(ca => ca.OwnershipRole)
               .IsRequired()
               .HasConversion<string>();


        builder.HasOne(ca => ca.Account)
               .WithMany(a => a.CustomerAccounts)
               .HasForeignKey(ca => ca.AccountId)
               .OnDelete(DeleteBehavior.Cascade);


    }
}
