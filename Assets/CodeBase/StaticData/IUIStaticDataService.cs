using System;
using CodeBase.UI.AbstractWindow;

namespace CodeBase.StaticData
{
    public interface IUIStaticDataService
    {
        T GetWindow<T>() where T : AbstractWindowBase;
    }
}