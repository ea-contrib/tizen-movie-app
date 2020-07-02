using System;
using System.Threading.Tasks;
using TMA.Data.Common;
using TMA.MessageBus;

namespace TMA.MovieService
{
    public class SaveChangesHandler: IPostExecuteHandler
    {
        private readonly DataContext _dataContext;
        public SaveChangesHandler(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task Handle()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
