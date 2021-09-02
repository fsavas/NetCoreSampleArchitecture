using Sample.Core.Defaults;
using System;

namespace Sample.Core.Domain.Logs
{
    public partial class Log : BaseEntity
    {
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string LogEvent { get; set; }
        public string User { get; set; }
        public LogKeys LogKey { get; set; }
    }
}