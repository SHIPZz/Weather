using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.ServersProcessing
{
    public class ServerApiService : IServerApiService
    {
        private readonly HttpClient _httpClient = new();

        public void Init()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Unity Weather App");
        }

        public async UniTask<string> GetApiResponseAsync(string url, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url,cancellationToken);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Debug.Log($"Server response: {result}");
                return result;
            }
            catch (HttpRequestException e)
            {
                Debug.LogError("Error requesting: " + e.Message);
                return null;
            }
        }
    }
}