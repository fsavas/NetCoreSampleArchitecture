using Sample.Core;
using Sample.Core.Domain.BackgroundJobs;
using System.Collections.Generic;

namespace Sample.Services.BackgroundJobs
{
    public partial interface ITaskScheduleService : IBaseService
    {
        void DeleteTaskSchedule(long taskScheduleId);

        List<TaskSchedule> GetAllTaskSchedules();

        IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch);

        TaskSchedule GetTaskScheduleById(long taskScheduleId);

        void InsertTaskSchedule(TaskSchedule taskSchedule);

        void UpdateTaskSchedule(TaskSchedule taskSchedule);

        string ExportTaskSchedules(TaskScheduleSearch taskScheduleSearch);
    }
}