using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Core.Domain.Users
{
    public partial class UserGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Users_UserGridModel_Username_DisplayName)]
        public string Username { get; set; }

        #endregion Properties
    }
}