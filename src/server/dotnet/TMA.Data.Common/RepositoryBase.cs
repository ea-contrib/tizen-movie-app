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
        private readonly DataContext _dataContext;
        public RepositoryBase(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected DbSet<T> GetTable<T>() where T: class
        {
            return _dataContext.Set<T>();
        }

    }
}
