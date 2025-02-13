using System;
using CodeBase.UI.AbstractWindow;
using Unity.VisualScripting;

public interface IController : IInitializable, IDisposable
{
}

public interface IController<TWindow> : IController where TWindow : AbstractWindowBase
{
    void BindView(TWindow value);
}

public interface IController<TModel, TWindow> : IController<TWindow>
    where TModel : AbstractWindowModel
    where TWindow : AbstractWindowBase
{
    void BindModel(TModel value);
}