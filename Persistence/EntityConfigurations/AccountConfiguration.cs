using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts").HasKey(account => account.Id);

        builder.Property(account => account.Id).HasColumnName("Id").IsRequired();
        builder.Property(account => account.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(account => account.AccountNumber).HasColumnName("AccountNumber").IsRequired();
        builder.Property(account => account.AccountType).HasColumnName("AccountType").IsRequired();
        builder.Property(account => account.Password).HasColumnName("Password").IsRequired();
        builder.Property(account => account.Balance).HasColumnName("Balance").IsRequired();
        builder.Property(account => account.Bank).HasColumnName("Bank").IsRequired();

        builder.Property(account => account.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(account => account.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(account => account.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: account => account.AccountNumber, name: "UK_Accounts_AccountNumber").IsUnique();

        builder.HasOne(account => account.User);

        builder.HasQueryFilter(account => !account.DeletedDate.HasValue);
    }
}