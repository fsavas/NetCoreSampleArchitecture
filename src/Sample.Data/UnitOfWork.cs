using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Logs;
using Sample.Core.Domain.Lookups;
using Sample.Core.Domain.Users;
using Sample.Core.Repository;
using Sample.Core.Repository.Logs;
using Sample.Core.Repository.Lookups;
using Sample.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly IAuditRepository _auditRepository;
        private readonly ILookupTableRepository _lookupTableRepository;

        #endregion Fields

        #region Constructor

        public UnitOfWork(IDbContext dbContext, IMemoryCache memoryCache, IAuditRepository auditRepository, ILookupTableRepository lookupTableRepository)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _auditRepository = auditRepository;
            _lookupTableRepository = lookupTableRepository;
        }

        #endregion Constructor

        #region Utilities

        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_dbContext is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _dbContext.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return GetUserFriendlyDbUpdateExceptionText(ex);
            }
        }

        private string GetUserFriendlyDbUpdateExceptionText(Exception exception)
        {
            var sqlException = exception.GetBaseException() as SqlException;

            if (sqlException != null)
            {
                string key = null;

                switch (sqlException.Number)
                {
                    case SqlExceptionTypes.UniqueIndexViolation:
                        key = MemoryCacheKeys.UniqueIndexViolation; //Bu kayıt daha önce girilmiştir
                        break;

                    case SqlExceptionTypes.UniqueConstraintViolation:
                        key = MemoryCacheKeys.UniqueConstraintViolation;
                        break;

                    case SqlExceptionTypes.ForeignKeyViolation:
                        key = MemoryCacheKeys.ForeignKeyViolation; //Bağlı kayıtlar var
                        break;

                    case SqlExceptionTypes.CannotInsertTheValueNull:
                        key = MemoryCacheKeys.CannotInsertTheValueNull;
                        break;
                }

                if (!string.IsNullOrEmpty(key) && _memoryCache.TryGetValue(key, out string message))
                    return message;
            }

            return exception.ToString();
        }

        #endregion Utilities

        #region Methods

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new BaseRepository<TEntity>(_dbContext);
        }

        public int SaveChanges(bool isTask = false)
        {
            try
            {
                AddAuditLogs(isTask);
                return _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
            //catch
            //{
            //    //DbEntityValidationException
            //    throw;
            //}
        }

        private void AddAuditLogs(bool isTask)
        {
            if (_dbContext is DbContext dbContext)// && _memoryCache.TryGetValue(MemoryCacheKeys.User, out User user))
            {
                User auditUser = null;
                dbContext.ChangeTracker.DetectChanges();
                List<AuditEntry> auditEntries = new List<AuditEntry>();

                foreach (EntityEntry entry in dbContext.ChangeTracker.Entries())
                {
                    if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    {
                        continue;
                    }

                    if (!isTask && _memoryCache.TryGetValue(MemoryCacheKeys.User, out User user))
                        auditUser = user;

                    var auditEntry = new AuditEntry(entry, auditUser);
                    auditEntries.Add(auditEntry);
                }

                if (auditEntries.Any())
                {
                    var logs = auditEntries.Select(x => x.ToAudit());
                    SetAuditTypes(logs);
                    _auditRepository.Insert(logs);
                }
            }
            //else
            //    throw new Exception(MemoryCacheKeys.NotLogin);
        }

        private void SetAuditTypes(IEnumerable<Audit> logs)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Normal);

            foreach (var log in logs)
            {
                if (EnumClasses.AuditTypes.Insert.Equals(log.AuditTypeEnum))
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Insert, out LookupTable lookupTable))
                        log.AuditType = lookupTable;
                    else
                    {
                        var type = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.AuditTypes, nameof(EnumClasses.AuditTypes.Insert));
                        log.AuditType = type;
                        _memoryCache.Set(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Insert, type, cacheOptions);
                    }
                }
                else if (EnumClasses.AuditTypes.Delete.Equals(log.AuditTypeEnum))
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Delete, out LookupTable lookupTable))
                        log.AuditType = lookupTable;
                    else
                    {
                        var type = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.AuditTypes, nameof(EnumClasses.AuditTypes.Delete));
                        log.AuditType = type;
                        _memoryCache.Set(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Delete, type, cacheOptions);
                    }
                }
                else if (EnumClasses.AuditTypes.Update.Equals(log.AuditTypeEnum))
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Update, out LookupTable lookupTable))
                        log.AuditType = lookupTable;
                    else
                    {
                        var type = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.AuditTypes, nameof(EnumClasses.AuditTypes.Update));
                        log.AuditType = type;
                        _memoryCache.Set(MemoryCacheKeys.EnumClasses_LookupTypes_AuditTypes_Update, type, cacheOptions);
                    }
                }
            }
        }

        #endregion Methods
    }
}