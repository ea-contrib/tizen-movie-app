using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TMA.Data.Common
{
    public class RepositoryBase
    {
        protected DataContext Context { get; }

        public RepositoryBase(DataContext dataContext)
        {
            Context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected DbSet<T> GetTable<T>() where T: class
        {
            return Context.Set<T>();
        }

    }
}
