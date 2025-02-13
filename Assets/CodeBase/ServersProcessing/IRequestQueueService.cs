using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.ServersProcessing
{
    public interface IRequestQueueService
    {
        void AddRequest(Func<CancellationToken, UniTask> request);
        void ClearQueue();
    }
}