using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CreditConfiguration : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.ToTable("Credits").HasKey(credit => credit.Id);

        builder.Property(credit => credit.Id).HasColumnName("Id").IsRequired();
        builder.Property(credit => credit.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(credit => credit.Name).HasColumnName("Name").IsRequired();
        builder.Property(credit => credit.RequestedLoanAmount).HasColumnName("RequestedLoanAmount").IsRequired();
        builder.Property(credit => credit.TotalPaymentAmount).HasColumnName("TotalPaymentAmount").IsRequired();
        builder.Property(credit => credit.MonthlyPaymentAmount).HasColumnName("MonthlyPaymentAmount").IsRequired();
        builder.Property(credit => credit.ReferredBank).HasColumnName("ReferredBank").IsRequired();
        builder.Property(credit => credit.MonthlyPaymentDate).HasColumnName("MonthlyPaymentDate").IsRequired();
        builder.Property(credit => credit.ApprovalStatus).HasColumnName("ApprovalStatus");

        builder.Property(credit => credit.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(credit => credit.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(credit => credit.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(credit => credit.User);

        builder.HasQueryFilter(credit => !credit.DeletedDate.HasValue);
    }
}