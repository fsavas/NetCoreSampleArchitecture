using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Logs;

namespace Sample.Data.Mapping.Logs
{
    public partial class LogMap : BaseEntityTypeConfiguration<Log>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Message);
            builder.Property(e => e.MessageTemplate);
            builder.Property(e => e.Level);
            builder.Property(e => e.TimeStamp).IsRequired();
            builder.Property(e => e.Exception);
            builder.Property(e => e.Properties).HasColumnType("xml");
            builder.Property(e => e.LogEvent);
            builder.Property(e => e.User);
            builder.Property(e => e.LogKey);

            base.Configure(builder);
        }

        #endregion Methods
    }
}