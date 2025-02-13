using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.ServersProcessing
{
    public interface IRequestQueueService
    {
        void AddRequest(Func<UniTask> request);
        bool IsProcessing();
        void ClearQueue();
    }
}