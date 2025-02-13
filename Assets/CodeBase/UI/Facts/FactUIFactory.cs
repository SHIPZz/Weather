using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Facts
{
    public class FactUIFactory : IFactUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public FactUIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public FactItemView CreateFactItem(Transform parent, FactData factData)
        {
          FactItemView itemPrefab =  _assetProvider.LoadAsset<FactItemView>(AssetPath.FactItemView);

          return _instantiator.InstantiatePrefabForComponent<FactItemView>(itemPrefab, parent)
              .With(item => item.Init(factData.ServerId, factData.Id.ToString(),factData.Name));
        }
    }
}