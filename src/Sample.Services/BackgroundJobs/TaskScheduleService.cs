using Sample.Core;
using Sample.Core.Domain.BackgroundJobs;
using Sample.Core.Repository.BackgroundJobs;
using Sample.Services.Events;
using Sample.Services.ExportImport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.BackgroundJobs
{
    public partial class TaskScheduleService : BaseService, ITaskScheduleService
    {
        #region Fields

        private readonly ITaskScheduleRepository _taskScheduleRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<TaskScheduleGrid, TaskSchedule> _exportManager;

        #endregion Fields

        #region Constructor

        public TaskScheduleService(IUnitOfWork unitOfWork, ITaskScheduleRepository taskScheduleRepository, IEventPublisher eventPublisher, IExportManager<TaskScheduleGrid, TaskSchedule> exportManager)
            : base(unitOfWork)
        {
            _taskScheduleRepository = taskScheduleRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteTaskSchedule(long taskScheduleId)
        {
            var taskSchedule = GetTaskScheduleById(taskScheduleId);

            if (taskSchedule == null)
                throw new ArgumentNullException(nameof(taskSchedule));

            _taskScheduleRepository.Delete(taskSchedule);
            _unitOfWork.SaveChanges();
        }

        public virtual List<TaskSchedule> GetAllTaskSchedules()
        {
            List<TaskSchedule> LoadTaskSchedulesFunc()
            {
                var query = from t in _taskScheduleRepository.Table orderby t.Id select t;
                return query.ToList();
            }

            return LoadTaskSchedulesFunc();
        }

        public virtual TaskSchedule GetTaskScheduleById(long taskScheduleId)
        {
            if (taskScheduleId == 0)
                return null;

            TaskSchedule LoadTaskScheduleFunc()
            {
                return _taskScheduleRepository.GetById(taskScheduleId);
            }

            return LoadTaskScheduleFunc();
        }

        public virtual void InsertTaskSchedule(TaskSchedule taskSchedule)
        {
            if (taskSchedule == null)
                throw new ArgumentNullException(nameof(taskSchedule));

            _taskScheduleRepository.Insert(taskSchedule);
            _unitOfWork.SaveChanges();

            _eventPublisher.EntityInserted(taskSchedule);
        }

        public virtual void UpdateTaskSchedule(TaskSchedule taskSchedule)
        {
            if (taskSchedule == null)
                throw new ArgumentNullException(nameof(taskSchedule));

            _taskScheduleRepository.Update(taskSchedule);
            _unitOfWork.SaveChanges();

            _eventPublisher.EntityUpdated(taskSchedule);
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<TaskSchedule> SearchTaskSchedules(TaskScheduleSearch taskScheduleSearch)
        {
            return _taskScheduleRepository.SearchTaskSchedules(taskScheduleSearch);
        }

        public string ExportTaskSchedules(TaskScheduleSearch taskScheduleSearch)
        {
            var list = (List<TaskSchedule>)_taskScheduleRepository.SearchAllTaskSchedules(taskScheduleSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}