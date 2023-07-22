using AuthModule.Context;
using AuthModule.DAL.Repositories;
using AuthModule.Models;
using AuthModule.Repos.IRepositories;

namespace AuthModule.Repos.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AuthContextDB DbCon) : base(DbCon)
        {
        }
    }
}
