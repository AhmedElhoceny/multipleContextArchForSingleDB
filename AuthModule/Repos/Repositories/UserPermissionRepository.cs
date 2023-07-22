using AuthModule.Context;
using AuthModule.DAL.Repositories;
using AuthModule.Models;
using AuthModule.Repos.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Repos.Repositories
{
    public class UserPermissionRepository : BaseRepository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(AuthContextDB DbCon) : base(DbCon)
        {
        }
    }
}
