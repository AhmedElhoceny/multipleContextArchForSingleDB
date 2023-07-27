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
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext DbCon) : base(DbCon)
        {
        }
    }
}
