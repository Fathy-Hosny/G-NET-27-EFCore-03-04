using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
{
    public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
    {
        builder.ToTable("IndividualCustomers");

        builder.Property(c => c.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.NationalId)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(c => c.NationalId)
               .IsUnique();

        builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasIndex(c => c.Email)
               .IsUnique();

        builder.Property(c => c.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(c => c.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.DateOfBirth)
               .IsRequired();
    }
}
