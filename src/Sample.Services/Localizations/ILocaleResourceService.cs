using Sample.Core;
using Sample.Core.Domain.Localizations;
using System.Collections.Generic;

namespace Sample.Services.Localizations
{
    public partial interface ILocaleResourceService : IBaseService
    {
        void DeleteLocaleResource(long localeResourceId);

        List<LocaleResource> GetAllLocaleResources();

        LocaleResource GetLocaleResourceById(long localeResourceId);

        void InsertLocaleResource(LocaleResource localeResource);

        void UpdateLocaleResource(LocaleResource localeResource);

        IPagedList<LocaleResource> SearchLocaleResources(LocaleResourceSearch localeResourceSearch);

        string ExportLocaleResources(LocaleResourceSearch localeResourceSearch);
    }
}