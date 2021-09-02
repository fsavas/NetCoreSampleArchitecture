namespace Sample.Core.Domain.Localizations
{
    public partial class Language : BaseEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public bool IsDeleted { get; set; }
    }
}