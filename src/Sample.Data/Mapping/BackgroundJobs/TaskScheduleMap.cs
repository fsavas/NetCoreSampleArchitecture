using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.BackgroundJobs;

namespace Sample.Data.Mapping.BackgroundJobs
{
    public partial class TaskScheduleMap : BaseEntityTypeConfiguration<TaskSchedule>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<TaskSchedule> builder)
        {
            builder.ToTable(nameof(TaskSchedule));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.NextRunOnSuccess).IsRequired();
            builder.Property(e => e.NextRunOnFailure).IsRequired();
            builder.Property(e => e.EntryDelay);
            builder.Property(e => e.EntryPeriod);
            builder.Property(e => e.IsStopOnError).IsRequired();
            builder.Property(e => e.LastStartTime);
            builder.Property(e => e.LastEndTime);
            builder.Property(e => e.IsRunning);

            builder.HasAlternateKey(e => e.Code);

            base.Configure(builder);
        }

        #endregion Methods
    }
}