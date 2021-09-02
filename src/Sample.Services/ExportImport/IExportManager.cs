using Sample.Core;
using System.Collections.Generic;

namespace Sample.Services.ExportImport
{
    public partial interface IExportManager<TModel, TEntity> where TModel : class where TEntity : BaseEntity
    {
        string ExportToExcel(List<TEntity> list);
    }
}