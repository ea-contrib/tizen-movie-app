using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMA.MessageBus
{
    public interface IErrorHandler
    {
        Task Handle(Exception ex);
    }

    public interface IPostExecuteHandler
    {
        Task Handle();
    }
}
