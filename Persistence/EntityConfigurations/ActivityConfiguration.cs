using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities").HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Id).HasColumnName("Id").IsRequired();
        builder.Property(transaction => transaction.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(transaction => transaction.Amount).HasColumnName("Amount").IsRequired();
        builder.Property(transaction => transaction.TransactionType).HasColumnName("TransactionType").IsRequired();
        builder.Property(transaction => transaction.TransactionDate).HasColumnName("TransactionDate").IsRequired();
        builder.Property(transaction => transaction.AccountNumber).HasColumnName("AccountNumber").IsRequired();
        builder.Property(transaction => transaction.TargetAccountNumber).HasColumnName("TargetAccountNumber");

        builder.Property(transaction => transaction.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(transaction => transaction.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(transaction => transaction.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(transaction => transaction.User);

        builder.HasQueryFilter(transaction => !transaction.DeletedDate.HasValue);
    }
}