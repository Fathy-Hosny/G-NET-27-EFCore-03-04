using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable("Managers");

        builder.HasKey(m => m.ManagerId);

        builder.Property(m => m.FullName)
               .IsRequired()
               .HasColumnType("nvarchar");

        builder.Property(m => m.Email)
               .IsRequired()
               .HasColumnType("nvarchar");
        builder.HasIndex(m => m.Email)
               .IsUnique();

        builder.Property(m => m.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(m => m.HireDate)
               .IsRequired();

        
        builder.HasOne(m => m.Branch)
               .WithOne(b => b.Managers)
               .HasForeignKey<Manager>(m => m.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => m.BranchId)
               .IsUnique();
    }
}
