using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.BackgroundJobs;
using Sample.Core.Repository.BackgroundJobs;
using Sample.Core.Repository.Localizations;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Services.BackgroundJobs.ScopedTasks
{
    public class ScopedSampleTask : IScopedProcessingService
    {
        #region Fields

        //private int executionCount = 0;
        private TaskSchedule taskSchedule;

        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILocaleResourceRepository _localeResourceRepository;
        private readonly ITaskScheduleRepository _taskScheduleRepository;

        #endregion Fields

        #region Constructor

        public ScopedSampleTask(IMemoryCache memoryCache, IUnitOfWork unitOfWork, ILanguageRepository languageRepository, ILocaleResourceRepository localeResourceRepository, ITaskScheduleRepository taskScheduleRepository)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _localeResourceRepository = localeResourceRepository;
            _languageRepository = languageRepository;
            _taskScheduleRepository = taskScheduleRepository;
        }

        #endregion Constructor

        #region Methods

        public async Task DoWork(CancellationToken cancellationToken)
        {
            Log.Logger.Information("ScopedSampleTask starting");

            while (!cancellationToken.IsCancellationRequested)// && executionCount == 0)
            {
                //executionCount++;
                try
                {
                    Log.Logger.Information("ScopedSampleTask working");

                    taskSchedule = GetTaskSchedule(nameof(ScopedSampleTask));

                    if (taskSchedule != null && taskSchedule.IsActive)
                    {
                        taskSchedule.IsRunning = true;
                        taskSchedule.LastStartTime = DateTime.Now;
                        _taskScheduleRepository.Update(taskSchedule);
                        _unitOfWork.SaveChanges(true);

                        SetLocaleResources();

                        taskSchedule.IsRunning = false;
                        taskSchedule.LastEndTime = DateTime.Now;
                        _taskScheduleRepository.Update(taskSchedule);
                        _unitOfWork.SaveChanges(true);

                        await Task.Delay(taskSchedule.NextRunOnSuccess, cancellationToken);
                    }
                    else
                    {
                        await Task.Delay(90000, cancellationToken);
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.Information("ScopedSampleTask error");

                    if(taskSchedule != null)
                        await Task.Delay(taskSchedule.NextRunOnFailure, cancellationToken);
                    else
                        await Task.Delay(90000, cancellationToken);
                }
            }
        }

        private TaskSchedule GetTaskSchedule(string code)
        {
            TaskSchedule currentTaskSchedule = null;

            try
            {
                if (_memoryCache.TryGetValue(code, out TaskSchedule cacheTaskSchedule))
                    currentTaskSchedule = cacheTaskSchedule;
                else
                {
                    //todo call sync data method to set the changes made in central system into local system db
                }
            }
            catch (Exception) { }
            finally
            {
                if (currentTaskSchedule == null)
                {
                    currentTaskSchedule = _taskScheduleRepository.GetByCode(code);

                    if (currentTaskSchedule != null)
                        _memoryCache.Set(code, currentTaskSchedule);
                }
            }

            return currentTaskSchedule;
        }

        private void SetLocaleResources()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Normal);

            var languages = _languageRepository.GetAllLanguages();

            foreach (var language in languages)
            {
                var languageId = language.Id;
                var localeResources = _localeResourceRepository.GetByLanguage(languageId);

                foreach (var localeResource in localeResources)
                {
                    _memoryCache.Set(string.Join(MemoryCacheKeys.KeySeperator, localeResource.Name, languageId), localeResource.Value, cacheOptions);
                }
            }
        }

        #endregion Methods
    }
}