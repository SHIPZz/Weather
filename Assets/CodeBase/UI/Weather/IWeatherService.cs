using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.UI.Weather
{
    public interface IWeatherService : IDisposable
    {
        IReadOnlyReactiveProperty<string> WeatherInfo { get; }
        UniTask LaunchWeatherContinuouslyRequestingAsync(CancellationToken cancellationToken);
        UniTask ProcessWeatherAsync(CancellationToken cancellationToken);
        void Cleanup();
    }
}