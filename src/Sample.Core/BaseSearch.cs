namespace Sample.Core
{
    public abstract partial class BaseSearch
    {
        #region Constructor

        public BaseSearch()
        {
            //set the default values
            Page = 1;
            PageSize = 10;
            OrderByAsc = false;
            OrderMember = "Id";
        }

        #endregion Constructor

        #region Properties

        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool OrderByAsc { get; set; }
        public string OrderMember { get; set; }

        #endregion Properties
    }
}