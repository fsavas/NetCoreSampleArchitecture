using Sample.Core;
using Sample.Core.Domain.Localizations;
using Sample.Core.Repository.Localizations;
using Sample.Services.Events;
using Sample.Services.ExportImport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Localizations
{
    public partial class LocaleResourceService : BaseService, ILocaleResourceService
    {
        #region Fields

        private readonly ILocaleResourceRepository _localeResourceRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<LocaleResourceGrid, LocaleResource> _exportManager;

        #endregion Fields

        #region Constructor

        public LocaleResourceService(IUnitOfWork unitOfWork, ILocaleResourceRepository localeResourceRepository, IEventPublisher eventPublisher, IExportManager<LocaleResourceGrid, LocaleResource> exportManager)
            : base(unitOfWork)
        {
            _localeResourceRepository = localeResourceRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteLocaleResource(long localeResourceId)
        {
            var localeResource = GetLocaleResourceById(localeResourceId);

            if (localeResource == null)
                throw new ArgumentNullException(nameof(localeResource));

            _localeResourceRepository.Update(localeResource);
            _unitOfWork.SaveChanges();
        }

        public virtual List<LocaleResource> GetAllLocaleResources()
        {
            List<LocaleResource> LoadLocaleResourcesFunc()
            {
                var query = from s in _localeResourceRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadLocaleResourcesFunc();
        }

        public virtual LocaleResource GetLocaleResourceById(long localeResourceId)
        {
            if (localeResourceId == 0)
                return null;

            LocaleResource LoadLocaleResourceFunc()
            {
                return _localeResourceRepository.GetById(localeResourceId);
            }

            return LoadLocaleResourceFunc();
        }

        public virtual void InsertLocaleResource(LocaleResource localeResource)
        {
            if (localeResource == null)
                throw new ArgumentNullException(nameof(localeResource));

            _localeResourceRepository.Insert(localeResource);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateLocaleResource(LocaleResource localeResource)
        {
            if (localeResource == null)
                throw new ArgumentNullException(nameof(localeResource));

            _localeResourceRepository.Update(localeResource);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<LocaleResource> SearchLocaleResources(LocaleResourceSearch localeResourceSearch)
        {
            return _localeResourceRepository.SearchLocaleResources(localeResourceSearch);
        }

        public string ExportLocaleResources(LocaleResourceSearch localeResourceSearch)
        {
            var list = (List<LocaleResource>)_localeResourceRepository.SearchAllLocaleResources(localeResourceSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}