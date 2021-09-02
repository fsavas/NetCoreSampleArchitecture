using System;

namespace Sample.Core.Domain.BackgroundJobs
{
    public partial class TaskSchedule : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int NextRunOnSuccess { get; set; }
        public int NextRunOnFailure { get; set; }
        public int EntryDelay { get; set; }
        public int EntryPeriod { get; set; }
        public bool IsStopOnError { get; set; }
        public DateTime? LastStartTime { get; set; }
        public DateTime? LastEndTime { get; set; }
        public bool IsRunning { get; set; }
    }
}