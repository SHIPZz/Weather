using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CodeBase.ServersProcessing
{
    public class RequestQueueService : IRequestQueueService
    {
        private readonly Queue<Func<UniTask>> _requestQueue = new();
        
        private bool _isProcessing = false;

        public void AddRequest(Func<UniTask> request)
        {
            _requestQueue.Enqueue(request);
            
            ProcessNext();
        }

        public void ClearQueue()
        {
            _requestQueue.Clear();
        }

        public bool IsProcessing() => _isProcessing;

        private async void ProcessNext()
        {
            if (_isProcessing || _requestQueue.Count == 0) 
                return;

            _isProcessing = true;
            
            Func<UniTask> request = _requestQueue.Dequeue();
            
            await request();
            
            _isProcessing = false;

            ProcessNext();
        }
    }
}