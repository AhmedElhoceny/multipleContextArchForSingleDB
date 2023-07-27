using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Repos.IRepositories
{
    public interface IAuthUnitOfWork : IDisposable
    {
        public ICompanyRepository CompanyRepository { get;}
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRolesRepository UserRoleRepository { get; }
        public IUserTokenRepository UserTokenRepository { get; }
        public IPermissionRepository PermissionRepository { get; }
        public IUserPermissionRepository UserPermissionRepository { get; }
        public Task<IDbContextTransaction> BeginTransaction();
        Task<int> Complete();
        void Dispose();
    }
}
