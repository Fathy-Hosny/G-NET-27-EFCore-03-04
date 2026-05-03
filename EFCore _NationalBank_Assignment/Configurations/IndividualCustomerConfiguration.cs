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
              .HasColumnType("varchar");

        builder.Property(c => c.NationalId)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(c => c.NationalId)
               .IsUnique();

        builder.Property(c => c.Email)
               .IsRequired();


        builder.HasIndex(c => c.Email)
               .IsUnique();

        builder.Property(c => c.Phone)
               .IsRequired();

        builder.Property(c => c.Address)
               .IsRequired();


        builder.Property(c => c.DateOfBirth)
               .IsRequired();
    }
}
