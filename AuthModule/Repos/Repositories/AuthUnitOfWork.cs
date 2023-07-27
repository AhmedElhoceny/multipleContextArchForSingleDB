using AuthModule.Context;
using AuthModule.Repos.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Repos.Repositories
{
    public class AuthUnitOfWork:IAuthUnitOfWork
    {
        public AuthContextDB _context { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }

        public IUserRepository UserRepository{ get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public IUserRolesRepository UserRoleRepository { get; set; }

        public IUserTokenRepository UserTokenRepository { get; set; }

        public IPermissionRepository PermissionRepository { get; set; }

        public IUserPermissionRepository UserPermissionRepository { get; set; }
        public AuthUnitOfWork(AuthContextDB context)
        {
            CompanyRepository = new CompanyRepository(context);
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
            UserRoleRepository = new UserRolesRepository(context);
            UserTokenRepository = new UserTokenRepository(context);
            PermissionRepository = new PermissionRepository(context);
            UserPermissionRepository = new UserPermissionRepository(context);
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
