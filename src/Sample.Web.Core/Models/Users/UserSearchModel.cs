using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Users
{
    public partial class UserSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Users_UserSearchModel_Username_DisplayName)]
        public string Username { get; set; }

        #endregion Properties
    }
}