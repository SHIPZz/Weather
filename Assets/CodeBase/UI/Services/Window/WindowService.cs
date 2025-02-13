using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using CodeBase.UI.AbstractWindow;
using Zenject;

namespace CodeBase.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IInstantiator _instantiator;
        private readonly IUIStaticDataService _uiStaticDataService;
        private readonly IUIProvider _uiProvider;
        
        private readonly Dictionary<Type, WindowBindingInfo> _windowBindings = new();
        
        private readonly Dictionary<Type, (AbstractWindowBase Window, IController Controller)> _activeWindows = new();

        public WindowService(IInstantiator instantiator, IUIStaticDataService uiStaticDataService, IUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
            _instantiator = instantiator;
            _uiStaticDataService = uiStaticDataService;
        }

        public void Initialize()
        {

        }
        
        public void Bind<TWindow, TController>()
            where TWindow : AbstractWindowBase
            where TController : IController<TWindow>
        {
            var windowType = typeof(TWindow);
            
            if (_windowBindings.ContainsKey(windowType))
                throw new InvalidOperationException($"Window type {windowType.Name} is already bound.");

            _windowBindings[windowType] = new WindowBindingInfo
            {
                WindowType = windowType,
                ControllerType = typeof(TController),
                ModelType = null,
                Prefab = _uiStaticDataService.GetWindow<TWindow>()
            };
        }
        
        public void Bind<TWindow, TController, TModel>()
            where TWindow : AbstractWindowBase
            where TModel : AbstractWindowModel
            where TController : IController<TModel, TWindow>
        {
            var windowType = typeof(TWindow);
            
            if (_windowBindings.ContainsKey(windowType))
                throw new InvalidOperationException($"Window type {windowType.Name} is already bound.");

            _windowBindings[windowType] = new WindowBindingInfo
            {
                WindowType = windowType,
                ControllerType = typeof(TController),
                ModelType = typeof(TModel),
                Prefab = _uiStaticDataService.GetWindow<TWindow>()
            };
        }
        
        public TWindow OpenWindow<TWindow>() where TWindow : AbstractWindowBase
        {
            var windowType = typeof(TWindow);

            if (!_windowBindings.TryGetValue(windowType, out var bindingInfo))
                throw new InvalidOperationException($"No binding found for window type {windowType.Name}");
            
            var window = _instantiator.InstantiatePrefabForComponent<TWindow>(bindingInfo.Prefab, _uiProvider.MainUI);

            AbstractWindowModel model = null;

            if (bindingInfo.ModelType != null)
                model = (AbstractWindowModel)_instantiator.Instantiate(bindingInfo.ModelType);

            IController<TWindow> controller = (IController<TWindow>)_instantiator.Instantiate(bindingInfo.ControllerType);

            if (controller is IController<AbstractWindowModel, TWindow> controllerWithModel && model != null)
                controllerWithModel.BindModel(model);

            controller.BindView(window);
            controller.Initialize();
            window.Open();
            
            _activeWindows[windowType] = (window, (controller));

            return window;
        }
        
        public void Hide<TWindow>() where TWindow : AbstractWindowBase
        {
            var windowType = typeof(TWindow);

            if (!_activeWindows.TryGetValue(windowType, out var windowData))
                return;
            
            windowData.Window.Close();

            if (windowData.Controller is IDisposable disposable)
                disposable.Dispose();

            _activeWindows.Remove(windowType);
        }

    }
}