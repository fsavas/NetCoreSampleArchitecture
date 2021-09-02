using Sample.Core.Domain.Users;

namespace Sample.Services.Users
{
    public partial interface IAuthenticationService : IBaseService
    {
        bool Login(User user);

        bool Logout();
    }
}