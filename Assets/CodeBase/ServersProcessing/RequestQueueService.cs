using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.ServersProcessing
{
    public class RequestQueueService : IRequestQueueService
    {
        private readonly Queue<Func<CancellationToken, UniTask>> _requestQueue = new();
        private bool _isProcessing;

        public void AddRequest(Func<CancellationToken, UniTask> request)
        {
            _requestQueue.Enqueue(request);
            ProcessNextAsync().Forget();
        }

        private async UniTaskVoid ProcessNextAsync()
        {
            try
            {
                if (_isProcessing || _requestQueue.Count == 0) 
                    return;

                _isProcessing = true;

                var request = _requestQueue.Dequeue();
                await request(CancellationToken.None);

                _isProcessing = false;
                ProcessNextAsync().Forget();
            }
            catch (Exception e)
            {
                
            }
        }

        public void ClearQueue()
        {
            _requestQueue.Clear();
        }
    }
}