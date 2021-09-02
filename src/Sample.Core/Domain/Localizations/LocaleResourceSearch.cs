namespace Sample.Core.Domain.Localizations
{
    public partial class LocaleResourceSearch : BaseSearch
    {
        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }
        public long LanguageId { get; set; }

        #endregion Properties
    }
}