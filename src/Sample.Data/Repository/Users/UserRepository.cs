using Sample.Core;
using Sample.Core.Domain.Users;
using Sample.Core.Helpers;
using Sample.Core.Repository.Users;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Constructor

        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<User> SearchUsers(UserSearch userSearch)
        {
            var query = Table;
            AddQueryCriteria(query, userSearch);

            return new PagedList<User>(query, userSearch.Page - 1, userSearch.PageSize);
        }

        public IList<User> SearchAllUsers(UserSearch userSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, userSearch);

            return query.ToList();
        }

        public List<User> GetAllUsers()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        public User GetByUsername(string username)
        {
            var query = from s in Table where s.Username == username & s.IsDeleted == false select s;

            return query.SingleOrDefault();
        }

        private IQueryable<User> AddQueryCriteria(IQueryable<User> query, UserSearch userSearch)
        {
            if (!string.IsNullOrEmpty(userSearch.Username))
                query = query.Where(s => s.Username.Contains(userSearch.Username));

            return LinqHelper<User>.OrderBy(query, userSearch.OrderMember, userSearch.OrderByAsc);
        }

        #endregion Methods
    }
}