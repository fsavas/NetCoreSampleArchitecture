using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Sample.Core.Domain.Logs;
using Sample.Core.Domain.Users;
using System;
using System.Collections.Generic;
using static Sample.Core.Defaults.EnumClasses;

namespace Sample.Data
{
    public class AuditEntry
    {
        #region Fields

        public EntityEntry _entry { get; }
        public AuditTypes _auditType { get; set; }
        public User _auditUser { get; set; }
        public string _tableName { get; set; }
        public Dictionary<string, object> _keyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> _oldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> _newValues { get; } = new Dictionary<string, object>();
        public List<string> _changedColumns { get; } = new List<string>();

        #endregion Fields

        #region Methods

        public AuditEntry(EntityEntry entry, User user)
        {
            _entry = entry;
            _auditUser = user;
            SetChanges();
        }

        private void SetChanges()
        {
            _tableName = _entry.Metadata.GetTableName();
            foreach (PropertyEntry property in _entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                string dbColumnName = property.Metadata.GetColumnName();

                if (property.Metadata.IsPrimaryKey())
                {
                    _keyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (_entry.State)
                {
                    case EntityState.Added:
                        _newValues[propertyName] = property.CurrentValue;
                        _auditType = AuditTypes.Insert;
                        break;

                    case EntityState.Deleted:
                        _oldValues[propertyName] = property.OriginalValue;
                        _auditType = AuditTypes.Delete;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            _changedColumns.Add(dbColumnName);

                            _oldValues[propertyName] = property.OriginalValue;
                            _newValues[propertyName] = property.CurrentValue;
                            _auditType = AuditTypes.Update;
                        }
                        break;
                }
            }
        }

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.AuditDateTime = DateTime.Now;
            audit.AuditTypeEnum = _auditType;//.ToString();
            audit.AuditUserId = _auditUser?.Id;
            audit.TableName = _tableName;
            audit.KeyValues = JsonConvert.SerializeObject(_keyValues);
            audit.OldValues = _oldValues.Count == 0 ? null : JsonConvert.SerializeObject(_oldValues);
            audit.NewValues = _newValues.Count == 0 ? null : JsonConvert.SerializeObject(_newValues);
            audit.ChangedColumns = _changedColumns.Count == 0 ? null : JsonConvert.SerializeObject(_changedColumns);

            return audit;
        }

        #endregion Methods
    }
}