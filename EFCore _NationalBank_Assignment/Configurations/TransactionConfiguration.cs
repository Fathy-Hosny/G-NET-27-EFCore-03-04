using BankManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankManagementSystem.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.TransactionId);

        builder.Property(t => t.TransactionType)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.Amount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(t => t.TransactionDate)
               .IsRequired();

        builder.Property(t => t.Description)
               .HasMaxLength(300);

     
        builder.HasOne(t => t.Account)
               .WithMany(a => a.Transactions)
               .HasForeignKey(t => t.AccountId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
