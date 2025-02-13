using System;
using System.Threading;
using CodeBase.Extensions;
using CodeBase.Gameplay.Dogs;
using CodeBase.ServersProcessing;
using CodeBase.UI.InfoPopup;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.UI.Facts
{
    public class FactWindowController : IController<FactWindow>
    {
        public FactWindow View { get; private set; }

        private readonly IDogService _dogService;
        private readonly IServerApiService _serverApiService;

        private CancellationTokenSource _cancellationToken = new();
        private readonly IWindowService _windowService;
        private string _lastFactId;

        public FactWindowController(IDogService dogService, IServerApiService serverApiService,
            IWindowService windowService)
        {
            _windowService = windowService;
            _serverApiService = serverApiService;
            _dogService = dogService;
        }

        public void Initialize()
        {
            View.Opened += InitView;
            View.FactSelected += ProcessFactSelection;
        }

        public void Dispose()
        {
            View.Opened -= InitView;
            View.FactSelected -= ProcessFactSelection;
            
            Cleanup();
        }

        private void InitView()
        {
            View.Init(_dogService.GetAll().AsFactDataList());
        }

        private async void ProcessFactSelection(string factId)
        {
            Cleanup();
            
            DogFactResponse dogFactResponse = null;

            try
            {
                _cancellationToken = new();
                string result = await _serverApiService.GetApiResponseAsync($"{ApiUrl.DogApiUrl}/{factId}", _cancellationToken.Token);
                dogFactResponse = JsonUtility.FromJson<DogFactResponse>(result);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                _dogService.SetLastSelectedDog(dogFactResponse?.data);
                View.StopItemLoadingAnimation(factId);

                if (dogFactResponse != null)
                    _windowService.OpenWindow<InfoPopupWindow>();
            }
        }

        public void BindView(FactWindow value)
        {
            View = value;
        }

        private void Cleanup()
        {
            View.StopItemsLoadingAnimation();
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}