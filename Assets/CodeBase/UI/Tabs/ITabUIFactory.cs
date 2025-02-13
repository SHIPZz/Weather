using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Tabs
{
    public interface ITabUIFactory
    {
        List<TabView> CreateAll(Transform parent);
    }
}