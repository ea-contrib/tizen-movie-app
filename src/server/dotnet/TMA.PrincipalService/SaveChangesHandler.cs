using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Data.Common;
using TMA.MessageBus;

namespace TMA.PrincipalService
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
