using Sample.Core;
using Sample.Core.Domain.BackgroundJobs;
using Sample.Core.Helpers;
using Sample.Core.Repository.BackgroundJobs;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.BackgroundJobs
{
    public class TaskScheduleRepository : BaseRepository<TaskSchedule>, ITaskScheduleRepository
    {
        public TaskScheduleRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, taskScheduleSearch);

            return new PagedList<TaskSchedule>(query, taskScheduleSearch.Page - 1, taskScheduleSearch.PageSize);
        }

        public IList<TaskSchedule> SearchAllTaskSchedules(TaskScheduleSearch taskScheduleSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, taskScheduleSearch);

            return query.ToList();
        }

        public List<TaskSchedule> GetAllTaskSchedules()
        {
            var query = from t in Table orderby t.Id select t;

            return query.ToList();
        }

        public TaskSchedule GetByCode(string code)
        {
            var query = from t in Table where t.Code == code select t;

            return query.SingleOrDefault();
        }

        private IQueryable<TaskSchedule> AddQueryCriteria(IQueryable<TaskSchedule> query, TaskScheduleSearch taskScheduleSearch)
        {
            if (!string.IsNullOrEmpty(taskScheduleSearch.Name))
                query = query.Where(s => s.Name.Contains(taskScheduleSearch.Name));

            return LinqHelper<TaskSchedule>.OrderBy(query, taskScheduleSearch.OrderMember, taskScheduleSearch.OrderByAsc);
        }
    }
}