using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.BackgroundJobs
{
    public partial class TaskScheduleSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}