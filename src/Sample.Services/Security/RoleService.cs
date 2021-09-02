using Sample.Core;
using Sample.Core.Domain.Security;
using Sample.Core.Repository.Security;
using Sample.Services.Events;
using Sample.Services.ExportImport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Roles
{
    public partial class RoleService : BaseService, IRoleService
    {
        #region Fields

        private readonly IRoleRepository _roleRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<RoleGrid, Role> _exportManager;

        #endregion Fields

        #region Constructor

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IEventPublisher eventPublisher, IExportManager<RoleGrid, Role> exportManager)
            : base(unitOfWork)
        {
            _roleRepository = roleRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteRole(long roleId)
        {
            var role = GetRoleById(roleId);

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.IsDeleted = true;
            _roleRepository.Update(role);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Role> GetAllRoles()
        {
            List<Role> LoadRolesFunc()
            {
                var query = from s in _roleRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadRolesFunc();
        }

        public virtual Role GetRoleById(long roleId)
        {
            if (roleId == 0)
                return null;

            Role LoadRoleFunc()
            {
                return _roleRepository.GetById(roleId);
            }

            return LoadRoleFunc();
        }

        public virtual void InsertRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _roleRepository.Insert(role);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _roleRepository.Update(role);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Role> SearchRoles(RoleSearch roleSearch)
        {
            return _roleRepository.SearchRoles(roleSearch);
        }

        public string ExportRoles(RoleSearch roleSearch)
        {
            var list = (List<Role>)_roleRepository.SearchAllRoles(roleSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}