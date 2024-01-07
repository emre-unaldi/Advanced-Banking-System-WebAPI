using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SupportConfiguration : IEntityTypeConfiguration<Support>
{
    public void Configure(EntityTypeBuilder<Support> builder)
    {
        builder.ToTable("Supports").HasKey(support => support.Id);

        builder.Property(support => support.Id).HasColumnName("Id").IsRequired();
        builder.Property(support => support.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(support => support.Title).HasColumnName("Title").IsRequired();
        builder.Property(support => support.Content).HasColumnName("Content").IsRequired();

        builder.Property(support => support.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(support => support.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(support => support.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: support => support.Title, name: "UK_Supports_Title").IsUnique();

        builder.HasOne(support => support.User);

        builder.HasQueryFilter(support => !support.DeletedDate.HasValue);
    }
}