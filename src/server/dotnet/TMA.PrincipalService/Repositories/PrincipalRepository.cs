using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMA.Data.Common;
using TMA.PrincipalService.Entities;

namespace TMA.PrincipalService.Repositories
{
    public class PrincipalRepository: RepositoryBase
    {
        public PrincipalRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public Task<PrincipalEntity> GetPrincipalByEmail(string email)
        {
            return GetTable<PrincipalEntity>()
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

        public Task<PrincipalEntity> GetById(int id)
        {
            return GetTable<PrincipalEntity>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
