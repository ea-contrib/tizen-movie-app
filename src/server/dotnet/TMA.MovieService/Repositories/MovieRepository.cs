using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TMA.Data.Common;
using TMA.MovieService.Entities;

namespace TMA.MovieService.Repositories
{
    public class MovieRepository: RepositoryBase
    {
        public MovieRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public Task<List<MovieEntity>> List(string providerId)
        {
            var query = GetTable<MovieEntity>().AsQueryable();
            if (providerId != null)
            {
                query = query.Where(x => x.ProviderId == providerId);
            }

            return query.ToListAsync();
        }


        public Task Update(MovieEntity entity)
        {
            GetTable<MovieEntity>().Update(entity);

            return Task.CompletedTask;
        }

        public async Task Insert(MovieEntity entity)
        {
            await GetTable<MovieEntity>().AddAsync(entity);
        }
    }
}
