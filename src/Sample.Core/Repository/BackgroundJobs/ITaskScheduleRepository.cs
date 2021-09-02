using Sample.Core.Domain.BackgroundJobs;
using System.Collections.Generic;

namespace Sample.Core.Repository.BackgroundJobs
{
    public partial interface ITaskScheduleRepository : IBaseRepository<TaskSchedule>
    {
        #region Methods

        IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        List<TaskSchedule> GetAllTaskSchedules();

        IList<TaskSchedule> SearchAllTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        TaskSchedule GetByCode(string code);

        #endregion Methods
    }
}