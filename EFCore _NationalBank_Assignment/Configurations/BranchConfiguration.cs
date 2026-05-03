using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.BranchId);

        builder.Property(b => b.BranchCode)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(b => b.BranchCode)
               .IsUnique();

        builder.Property(b => b.BranchName)
               .IsRequired()
               .HasColumnType("nvarchar");

        builder.Property(b => b.City)
               .IsRequired()
               .HasColumnType("nvarchar");

        builder.Property(b => b.Address)
               .IsRequired()
               .HasColumnType("nvarchar");

        builder.Property(b => b.Phone)
               .IsRequired()
               .HasMaxLength(20);
    }
}
