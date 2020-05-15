using System.Threading;
using System.Threading.Tasks;

namespace TMA.MessageBus
{
    public static class TaskCompletionSourceExtensions
    {
        public static async Task<TResult> WaitAsync<TResult>(this TaskCompletionSource<TResult> tcs, CancellationToken cancelToken)
        {
            using (cancelToken.Register(() => {
                tcs.TrySetCanceled();
            }))
            {
                return await tcs.Task.ConfigureAwait(false);
            }
        }
    }
}
