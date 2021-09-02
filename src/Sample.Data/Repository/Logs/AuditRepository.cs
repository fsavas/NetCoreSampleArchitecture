using Sample.Core.Domain.Logs;
using Sample.Core.Repository.Logs;

namespace Sample.Data.Repository.Logs
{
    public class AuditRepository : BaseRepository<Audit>, IAuditRepository
    {
        #region Constructor

        public AuditRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}