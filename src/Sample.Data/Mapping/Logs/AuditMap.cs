using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Logs;

namespace Sample.Data.Mapping.Logs
{
    public partial class AuditMap : BaseEntityTypeConfiguration<Audit>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable(nameof(Audit));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);
            builder.Property(e => e.AuditDateTime);
            builder.Property(e => e.TableName);
            builder.Property(e => e.KeyValues);
            builder.Property(e => e.OldValues);
            builder.Property(e => e.NewValues);
            builder.Property(e => e.ChangedColumns);

            builder.HasOne(e => e.AuditUser)
                .WithMany()
                .HasForeignKey(e => e.AuditUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.AuditType)
                .WithMany()
                .HasForeignKey(e => e.AuditTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.AuditTypeEnum);
        }

        #endregion Methods
    }
}