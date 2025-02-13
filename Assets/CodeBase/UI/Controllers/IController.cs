using System;
using CodeBase.UI.AbstractWindow;
using Unity.VisualScripting;

public interface IController : IInitializable, IDisposable
{
}

public interface IController<TWindow> : IController where TWindow : AbstractWindowBase
{
    TWindow View { get; }
    void BindView(TWindow value);
}

public interface IController<TModel, TWindow> : IController<TWindow>
    where TModel : AbstractWindowModel
    where TWindow : AbstractWindowBase
{
    TModel Model { get; }
    void BindModel(TModel value);
}