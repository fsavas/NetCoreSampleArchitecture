using Sample.Core.Defaults;
using System;
using System.ComponentModel;

namespace Sample.Web.Core.Models.BackgroundJobs
{
    public partial class TaskScheduleGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnSuccess_DisplayName)]
        public long NextRunOnSuccess { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnFailure_DisplayName)]
        public long NextRunOnFailure { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryDelay_DisplayName)]
        public int EntryDelay { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryPeriod_DisplayName)]
        public int EntryPeriod { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsStopOnError_DisplayName)]
        public bool IsStopOnError { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastStartTime_DisplayName)]
        public DateTime? LastStartTime { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastEndTime_DisplayName)]
        public DateTime? LastEndTime { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsRunning_DisplayName)]
        public bool IsRunning { get; set; }

        #endregion Properties
    }
}