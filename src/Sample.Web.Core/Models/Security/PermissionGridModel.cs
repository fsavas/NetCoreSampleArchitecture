﻿using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Security
{
    public partial class PermissionGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}