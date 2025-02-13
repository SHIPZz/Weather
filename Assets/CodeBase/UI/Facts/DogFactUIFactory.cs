using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Facts
{
    public class DogDogFactUIFactory : IDogFactUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public DogDogFactUIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public DogFactItemView CreateFactItem(Transform parent, DogFactData dogFactData)
        {
          DogFactItemView itemPrefab =  _assetProvider.LoadAsset<DogFactItemView>(AssetPath.FactItemView);

          return _instantiator.InstantiatePrefabForComponent<DogFactItemView>(itemPrefab, parent)
              .With(item => item.Init(dogFactData.ServerId, dogFactData.Id.ToString(),dogFactData.Name));
        }
    }
}