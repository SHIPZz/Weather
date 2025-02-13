using CodeBase.UI.AbstractWindow;

namespace CodeBase.UI.Services.Window
{
    public interface IWindowService
    {
        void Bind<TWindow, TController>()
            where TWindow : AbstractWindowBase
            where TController : IController<TWindow>;

        void Bind<TWindow, TController, TModel>()
            where TWindow : AbstractWindowBase
            where TModel : AbstractWindowModel
            where TController : IController<TModel, TWindow>;

        TWindow OpenWindow<TWindow>() where TWindow : AbstractWindowBase;
        void Hide<TWindow>() where TWindow : AbstractWindowBase;
        void Initialize();

        TWindow GetWindow<TWindow>() where TWindow : AbstractWindowBase;
    }
}