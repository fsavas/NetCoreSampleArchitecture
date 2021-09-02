using Sample.Core;
using Sample.Core.Domain.Users;
using Sample.Core.Encryption;
using Sample.Core.Repository.Users;
using Sample.Services.Events;
using Sample.Services.ExportImport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Users
{
    public partial class UserService : BaseService, IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEncryptionManager _encryptionManager;
        private readonly IExportManager<UserGrid, User> _exportManager;

        #endregion Fields

        #region Constructor

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IEventPublisher eventPublisher, IEncryptionManager encryptionManager, IExportManager<UserGrid, User> exportManager)
            : base(unitOfWork)
        {
            _userRepository = userRepository;
            _eventPublisher = eventPublisher;
            _encryptionManager = encryptionManager;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteUser(long userId)
        {
            var user = GetUserById(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.IsDeleted = true;
            _userRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public virtual List<User> GetAllUsers()
        {
            List<User> LoadUsersFunc()
            {
                var query = from s in _userRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadUsersFunc();
        }

        public virtual User GetUserById(long userId)
        {
            if (userId == 0)
                return null;

            User LoadUserFunc()
            {
                return _userRepository.GetById(userId);
            }

            return LoadUserFunc();
        }

        public virtual void InsertUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            CreateSaltKey(user);

            _userRepository.Insert(user);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            CreateSaltKey(user);

            _userRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<User> SearchUsers(UserSearch userSearch)
        {
            return _userRepository.SearchUsers(userSearch);
        }

        public string ExportUsers(UserSearch userSearch)
        {
            var list = (List<User>)_userRepository.SearchAllUsers(userSearch);

            return _exportManager.ExportToExcel(list);
        }

        private void CreateSaltKey(User user)
        {
            var encryptedData = _encryptionManager.CreateSaltKey(user.Password);

            if (encryptedData != null)
            {
                user.Key = encryptedData.Key;
                user.Salt = encryptedData.Salt;
            }
        }

        #endregion Methods
    }
}