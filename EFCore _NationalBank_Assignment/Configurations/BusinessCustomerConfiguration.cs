using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class BusinessCustomerConfiguration : IEntityTypeConfiguration<BusinessCustomer>
{
    public void Configure(EntityTypeBuilder<BusinessCustomer> builder)
    {

        builder.ToTable("BusinessCustomers");

        builder.Property(c => c.FullName)
               .IsRequired()
               .HasColumnType("nvarchar");

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

        builder.Property(c => c.CompanyName)
               .IsRequired()
               .HasColumnType("nvarchar");

        builder.Property(c => c.TaxNumber)
               .IsRequired()
               .HasColumnType("nvarchar");
        builder.HasIndex(c => c.TaxNumber)
               .IsUnique();
    }
}
