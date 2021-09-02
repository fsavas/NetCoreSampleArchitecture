using Sample.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Models.BackgroundJobs
{
    public partial class TaskScheduleModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_DisplayName)]
        public long NextRunOnSuccess { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_DisplayName)]
        public long NextRunOnFailure { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_DisplayName)]
        public int EntryDelay { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_DisplayName)]
        public int EntryPeriod { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_DisplayName)]
        public bool IsStopOnError { get; set; }

        #endregion Properties
    }
}