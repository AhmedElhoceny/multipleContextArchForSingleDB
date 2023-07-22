using AuthModule.DAL.IRepositories;
using AuthModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Repos.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
