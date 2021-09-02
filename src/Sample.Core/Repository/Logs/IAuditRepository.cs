using Sample.Core.Domain.Logs;

namespace Sample.Core.Repository.Logs
{
    public partial interface IAuditRepository : IBaseRepository<Audit>
    {
    }
}