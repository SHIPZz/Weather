using System;
using CodeBase.UI.AbstractWindow;
using Unity.VisualScripting;

namespace CodeBase.UI.Controllers
{
    public interface IController : IInitializable, IDisposable
    {
    }

    public interface IController<in TWindow> : IController where TWindow : AbstractWindowBase
    {
        void BindView(TWindow value);
    }
    
    public interface IModelBindable
    {
        void BindModel(AbstractWindowModel model);
    }
}