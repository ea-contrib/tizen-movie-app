using System;
using System.Threading;
using System.Threading.Tasks;

namespace TMA.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<TRequest>(TRequest request);
        Task<TResult> PublishAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default);

        Task<TResult> PublishAsync<TRequest, TResult>(CancellationToken cancellationToken = default)
            where TRequest : new();
        Task SubscribeAsync<TRequest, TResult>(Func<TRequest, Task<TResult>> consumer);
        Task SubscribeAsync<TRequest>(Func<TRequest, Task> process);
    }
}
