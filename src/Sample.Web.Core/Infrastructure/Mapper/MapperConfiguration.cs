using AutoMapper;
using Sample.Core;
using Sample.Core.Domain.BackgroundJobs;
using Sample.Core.Domain.Localizations;
using Sample.Core.Domain.Lookups;
using Sample.Core.Domain.Security;
using Sample.Core.Domain.Users;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.BackgroundJobs;
using Sample.Web.Core.Models.Localizations;
using Sample.Web.Core.Models.Lookups;
using Sample.Web.Core.Models.Security;
using Sample.Web.Core.Models.Users;

namespace Sample.Web.Infrastructure.Mapper
{
    public class MapperConfiguration : Profile
    {
        #region Constructor

        public MapperConfiguration()
        {
            //create all of the maps
            CreateMaps();
        }

        #endregion Constructor

        #region Utilities

        private void CreateMaps()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserSearchModel, UserSearch>();
            CreateMap<User, UserGrid>();
            CreateMap<User, UserGridModel>();

            CreateMap<Language, LanguageModel>();
            CreateMap<LanguageModel, Language>();
            CreateMap<LanguageSearchModel, LanguageSearch>();
            CreateMap<Language, LanguageGrid>();
            CreateMap<Language, LanguageGridModel>();

            CreateMap<LocaleResource, LocaleResourceModel>();
            CreateMap<LocaleResourceModel, LocaleResource>();
            CreateMap<LocaleResourceSearchModel, LocaleResourceSearch>();
            CreateMap<LocaleResource, LocaleResourceGrid>()
                .ForMember(dto => dto.Language_Name, conf => conf.MapFrom(e => e.Language.Name));
            CreateMap<LocaleResource, LocaleResourceGridModel>()
                .ForMember(dto => dto.Language_Name, conf => conf.MapFrom(e => e.Language.Name));

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
            CreateMap<RoleSearchModel, RoleSearch>();
            CreateMap<Role, RoleGrid>();
            CreateMap<Role, RoleGridModel>();

            CreateMap<Permission, PermissionModel>();
            CreateMap<PermissionModel, Permission>();
            CreateMap<PermissionSearchModel, PermissionSearch>();
            CreateMap<Permission, PermissionGrid>();
            CreateMap<Permission, PermissionGridModel>();

            CreateMap<TaskSchedule, TaskScheduleModel>();
            CreateMap<TaskScheduleModel, TaskSchedule>();
            CreateMap<TaskScheduleSearchModel, TaskScheduleSearch>();
            CreateMap<TaskSchedule, TaskScheduleGrid>();
            CreateMap<TaskSchedule, TaskScheduleGridModel>();

            CreateMap<LookupTable, LookupTableModel>();
            CreateMap<LookupTableModel, LookupTable>();
            CreateMap<LookupTableSearchModel, LookupTableSearch>();
            CreateMap<LookupTable, LookupTableGrid>();
            CreateMap<LookupTable, LookupTableGridModel>();

            CreateMap<SelectListItem, SelectListItemModel>();
            CreateMap<SelectListItemModel, SelectListItem>();
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListMapper<,>));
        }

        #endregion Utilities
    }
}