using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.BranchCode);

        builder.Property(b => b.BranchCode)
               .HasMaxLength(20);

        builder.Property(b => b.BranchName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.City)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.Address)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(b => b.Phone)
               .IsRequired()
               .HasMaxLength(20);
    }
}
