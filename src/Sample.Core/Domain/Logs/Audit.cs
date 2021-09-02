using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using Sample.Core.Domain.Users;
using System;

namespace Sample.Core.Domain.Logs
{
    public partial class Audit : BaseEntity
    {
        public DateTime AuditDateTime { get; set; }  //Log time
        public virtual LookupTable AuditType { get; set; }           //Process type
        public virtual User AuditUser { get; set; }           //Log User
        public string TableName { get; set; }           //Table where rows been created/updated/deleted
        public string KeyValues { get; set; }           //Pk and it's values
        public string OldValues { get; set; }           //Changed column name and old value
        public string NewValues { get; set; }           //Changed column name and current value
        public string ChangedColumns { get; set; }      //Changed column names
        public long? AuditTypeId { get; set; }
        public long? AuditUserId { get; set; }
        public EnumClasses.AuditTypes AuditTypeEnum { get; set; }
    }
}