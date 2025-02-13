using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Gameplay.Dogs
{
    public interface IDogService
    {
        IEnumerable<DogFact> GetAll();
        UniTask GetDogFactsAsync(CancellationToken cancellationToken);
        UniTask<DogFact> GetDogFactAsync(string id, CancellationToken cancellationToken);
        IReadOnlyReactiveProperty<DogFact> DogFact { get; }
    }
}