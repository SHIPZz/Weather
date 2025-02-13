using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CodeBase.ServersProcessing;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Dogs
{
    public class DogService : IDogService
    {
        private const int MaxDogCount = 10;
        
        private readonly IServerApiService _serverApiService;
        private readonly Dictionary<string, DogFact> _dogs = new();
        private readonly ReactiveProperty<DogFact> _dogFact = new();

        public IReadOnlyReactiveProperty<DogFact> DogFact => _dogFact;

        public DogService(IServerApiService serverApiService)
        {
          _serverApiService = serverApiService ?? throw new ArgumentNullException(nameof(serverApiService));
        }

        public void Add(IEnumerable<DogFact> dogData)
        {
            foreach (var dogFact in dogData)
            {
                _dogs[dogFact.id] = dogFact;
            }
        }

        public async UniTask GetDogFactsAsync(CancellationToken cancellationToken)
        {
            try
            {
                string result = await _serverApiService.GetApiResponseAsync(ApiUrl.DogApiUrl, cancellationToken);
                DogFactsResponse dogFactsResponse = JsonUtility.FromJson<DogFactsResponse>(result);

                if (dogFactsResponse != null && dogFactsResponse.data.Count > 0)
                {
                    IEnumerable<DogFact> targetDogs = dogFactsResponse.data.Take(MaxDogCount);

                    Add(targetDogs);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Request dog fact was canceled");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Request dog fact error: {ex.Message}");
            }
        }

        public async UniTask<DogFact> GetDogFactAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                string result = await _serverApiService.GetApiResponseAsync($"{ApiUrl.DogApiUrl}/{id}", cancellationToken);
                var dogFactResponse = JsonUtility.FromJson<DogFactResponse>(result);
                return dogFactResponse.data;
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;
        }

        public IEnumerable<DogFact> GetAll() => _dogs.Values;
    }
}