using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.ServersProcessing
{
    public interface IServerApiService
    {
        UniTask<string> GetApiResponseAsync(string url, CancellationToken cancellationToken);
        void Init();
    }
}