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
    public class GrantRepository: RepositoryBase
    {
        public GrantRepository(DataContext dataContext) : base(dataContext)
        {
        }


        public Task<List<GrantEntity>> List(string key, string subjectId, string type, string clientId)
        {
            var query = GetTable<GrantEntity>().AsQueryable();

            if (key != null)
            {
                query = query.Where(x => x.Key == key);
            }

            if (subjectId != null)
            {
                query = query.Where(x => x.SubjectId == subjectId);
            }

            if (type != null)
            {
                query = query.Where(x => x.Type == type);
            }

            if (clientId != null)
            {
                query = query.Where(x => x.ClientId == clientId);
            }


            return query.ToListAsync();
        }

        public Task Update(GrantEntity entity)
        {
            GetTable<GrantEntity>().Update(entity);

            return Task.CompletedTask;
        }

        public Task Remove(List<GrantEntity> grants)
        {
            GetTable<GrantEntity>().RemoveRange(grants);

            return Task.CompletedTask;
        }

        public async Task Insert(GrantEntity entity)
        {
            await GetTable<GrantEntity>().AddAsync(entity);
        }
    }
}
