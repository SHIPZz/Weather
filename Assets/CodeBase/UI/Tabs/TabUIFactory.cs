using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Tabs
{
    public class TabUIFactory : ITabUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public TabUIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public List<TabView> CreateAll(Transform parent)
        {
           var tabPrefabs =  _assetProvider.LoadAllAssets<TabView>(AssetPath.Tabs);
           var tabViews = new List<TabView>();

           foreach (TabView tabPrefab in tabPrefabs)
           {
               TabView createdTab = _instantiator.InstantiatePrefabForComponent<TabView>(tabPrefab, parent);
               tabViews.Add(createdTab);
           }

           return tabViews;
        }
    }
}