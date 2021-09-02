using System.ComponentModel;

namespace Sample.Core.Defaults
{
    public static class EnumClasses
    {
        public enum FileExtensionTypes
        {
            [Description("xlsx")]
            xlsx = 1
        }

        public enum FileMediaTypes
        {
            [Description("application/octet-stream")]
            OctetStream = 1
        }

        public enum AuditTypes
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum LookupTypes
        {
            AuditTypes = 1
        }
    }
}