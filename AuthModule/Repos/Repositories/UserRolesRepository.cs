using AuthModule.Context;
using AuthModule.DAL.Repositories;
using AuthModule.Models;
using AuthModule.Repos.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Repos.Repositories
{
    public class UserRolesRepository:BaseRepository<UserRole>,IUserRolesRepository
    {
        public UserRolesRepository(AuthContextDB DbCon) : base(DbCon)
        {
        }
    }
}
