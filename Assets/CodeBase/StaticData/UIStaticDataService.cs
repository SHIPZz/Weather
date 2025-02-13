using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Constants;
using CodeBase.UI.AbstractWindow;
using UnityEngine;

namespace CodeBase.StaticData
{
    public interface IUIStaticDataService
    {
        T GetWindow<T>(Type windowType) where T : AbstractWindowBase;
        T GetWindow<T>() where T : AbstractWindowBase;
    }

    public class UIStaticDataService : IUIStaticDataService
    {
        private readonly Dictionary<Type, AbstractWindowBase> _windows;

        public UIStaticDataService()
        {
            _windows = Resources.LoadAll<AbstractWindowBase>(AssetPath.Windows)
                .ToDictionary(x => x.GetType(), x => x);
        }

        public T GetWindow<T>(Type windowType) where T : AbstractWindowBase
        {
            if (!_windows.TryGetValue(windowType, out AbstractWindowBase windowPrefab))
                throw new Exception();

            return (T)windowPrefab;
        }
    
        public T GetWindow<T>() where T : AbstractWindowBase
        {
            if (!_windows.TryGetValue(typeof(T), out AbstractWindowBase windowPrefab))
                throw new Exception();

            return (T)windowPrefab;
        }
        
    }
}