using Sample.Core;
using Sample.Core.Domain.Users;
using System.Collections.Generic;

namespace Sample.Services.Users
{
    public partial interface IUserService : IBaseService
    {
        void DeleteUser(long userId);

        List<User> GetAllUsers();

        User GetUserById(long userId);

        void InsertUser(User user);

        void UpdateUser(User user);

        IPagedList<User> SearchUsers(UserSearch userSearch);

        string ExportUsers(UserSearch userSearch);
    }
}