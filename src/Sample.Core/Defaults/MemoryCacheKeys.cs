namespace Sample.Core.Defaults
{
    public static class MemoryCacheKeys
    {
        public const string User = "User";
        public const string Language = "Language";
        public const string Permissions = "Permissions";
        public const string KeySeperator = "-";
        public const string Sheet = "Sheet";
        public const string DashboardAccessToken = "DashboardAccessToken";
        public const string DashboardExpiration = "DashboardExpiration";

        #region UI Messages

        public const string NotLogin = "NotLogin";
        public const string UniqueIndexViolation = "UniqueIndexViolation";
        public const string UniqueConstraintViolation = "UniqueConstraintViolation";
        public const string ForeignKeyViolation = "ForeignKeyViolation";
        public const string CannotInsertTheValueNull = "CannotInsertTheValueNull";
        public const string ControllerActionSuccess = "ControllerActionSuccess";
        public const string InvalidUsernamePassword = "InvalidUsernamePassword";
        public const string LogoutUnsuccessful = "LogoutUnsuccessful";
        public const string PermissionDenied = "PermissionDenied";

        #endregion UI Messages

        #region Enums

        public const string EnumClasses_LookupTypes_AuditTypes_Insert = "EnumClasses_LookupTypes_AuditTypes_Insert";
        public const string EnumClasses_LookupTypes_AuditTypes_Update = "EnumClasses_LookupTypes_AuditTypes_Update";
        public const string EnumClasses_LookupTypes_AuditTypes_Delete = "EnumClasses_LookupTypes_AuditTypes_Delete";

        #endregion Enums

        #region Models

        public const string Sample_Web_Core_Models_Users_UserModel_Username_DisplayName = "Sample_Web_Core_Models_Users_UserModel_Username_DisplayName";
        public const string Sample_Web_Core_Models_Users_UserModel_Username_Required = "Sample_Web_Core_Models_Users_UserModel_Username_Required";
        public const string Sample_Web_Core_Models_Users_UserModel_Password_DisplayName = "Sample_Web_Core_Models_Users_UserModel_Password_DisplayName";
        public const string Sample_Web_Core_Models_Users_UserModel_Password_Required = "Sample_Web_Core_Models_Users_UserModel_Password_Required";
        public const string Sample_Web_Core_Models_Users_UserModel_Password_RegularExpression = "Sample_Web_Core_Models_Users_UserModel_Password_RegularExpression";
        public const string Sample_Web_Core_Models_Users_UserModel_AvailableRoles_DisplayName = "Sample_Web_Core_Models_Users_UserModel_AvailableRoles_DisplayName";
        public const string Sample_Web_Core_Models_Users_UserModel_SelectedRoles_Required = "Sample_Web_Core_Models_Users_UserModel_SelectedRoles_Required";
        public const string Sample_Web_Core_Models_Users_UserModel_SelectedRoles_DisplayName = "Sample_Web_Core_Models_Users_UserModel_SelectedRoles_DisplayName";
        public const string Sample_Web_Core_Models_Users_UserSearchModel_Username_DisplayName = "Sample_Web_Core_Models_Users_UserSearchModel_Username_DisplayName";
        public const string Sample_Web_Core_Models_Users_UserGridModel_Username_DisplayName = "Sample_Web_Core_Models_Users_UserGridModel_Username_DisplayName";

        public const string Sample_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LanguageModel_Name_Required = "Sample_Web_Core_Models_Localizations_LanguageModel_Name_Required";
        public const string Sample_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName = "Sample_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LanguageModel_Culture_Required = "Sample_Web_Core_Models_Localizations_LanguageModel_Culture_Required";
        public const string Sample_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName = "Sample_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName";

        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required = "Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName";
        public const string Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName = "Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName";

        public const string Sample_Web_Core_Models_Security_RoleModel_Code_Required = "Sample_Web_Core_Models_Security_RoleModel_Code_Required";
        public const string Sample_Web_Core_Models_Security_RoleModel_Code_DisplayName = "Sample_Web_Core_Models_Security_RoleModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_Security_RoleModel_Name_Required = "Sample_Web_Core_Models_Security_RoleModel_Name_Required";
        public const string Sample_Web_Core_Models_Security_RoleModel_Name_DisplayName = "Sample_Web_Core_Models_Security_RoleModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName = "Sample_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Security_RoleGridModel_Code_DisplayName = "Sample_Web_Core_Models_Security_RoleGridModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_Security_RoleGridModel_Name_DisplayName = "Sample_Web_Core_Models_Security_RoleGridModel_Name_DisplayName";

        public const string Sample_Web_Core_Models_Security_PermissionModel_Code_Required = "Sample_Web_Core_Models_Security_PermissionModel_Code_Required";
        public const string Sample_Web_Core_Models_Security_PermissionModel_Code_DisplayName = "Sample_Web_Core_Models_Security_PermissionModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_Security_PermissionModel_Name_Required = "Sample_Web_Core_Models_Security_PermissionModel_Name_Required";
        public const string Sample_Web_Core_Models_Security_PermissionModel_Name_DisplayName = "Sample_Web_Core_Models_Security_PermissionModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName = "Sample_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName = "Sample_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName";
        public const string Sample_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName = "Sample_Web_Core_Models_Security_PermissionGridModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName = "Sample_Web_Core_Models_Security_PermissionGridModel_Name_DisplayName";

        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsActive_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnSuccess_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_NextRunOnFailure_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryDelay_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_EntryPeriod_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_Required = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_Required";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleModel_IsStopOnError_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Code_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Code_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Name_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsActive_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsActive_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnSuccess_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnSuccess_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnFailure_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_NextRunOnFailure_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryDelay_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryDelay_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryPeriod_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_EntryPeriod_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsStopOnError_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsStopOnError_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastStartTime_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastStartTime_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastEndTime_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_LastEndTime_DisplayName";
        public const string Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsRunning_DisplayName = "Sample_Web_Core_Models_BackgroundJobs_TaskScheduleGridModel_IsRunning_DisplayName";

        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_LookupType_Required = "Sample_Web_Core_Models_Lookups_LookupTableModel_LookupType_Required";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Name_Required = "Sample_Web_Core_Models_Lookups_LookupTableModel_Name_Required";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Name_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableModel_Name_DisplayName";

        //public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Value_Required = "Sample_Web_Core_Models_Lookups_LookupTableModel_Value_Required";
        //public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Value_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableModel_Value_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Description_Required = "Sample_Web_Core_Models_Lookups_LookupTableModel_Description_Required";

        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_Description_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableModel_Description_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_IsActive_Required = "Sample_Web_Core_Models_Lookups_LookupTableModel_IsActive_Required";
        public const string Sample_Web_Core_Models_Lookups_LookupTableModel_IsActive_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableModel_IsActive_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableGridModel_LookupType_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableGridModel_LookupType_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableGridModel_Name_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableGridModel_Name_DisplayName";

        //public const string Sample_Web_Core_Models_Lookups_LookupTableGridModel_Value_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableGridModel_Value_DisplayName";
        public const string Sample_Web_Core_Models_Lookups_LookupTableGridModel_Description_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableGridModel_Description_DisplayName";

        public const string Sample_Web_Core_Models_Lookups_LookupTableGridModel_IsActive_DisplayName = "Sample_Web_Core_Models_Lookups_LookupTableGridModel_IsActive_DisplayName";

        #endregion Models
    }
}