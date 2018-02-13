using FileTag.MVVM;
using FileTag.ViewModels;
using FileTag.Views.Windows;
using Ninject;
using System;

namespace FileTag.Infrastacture
{
    public class DIContainer : IDisposable
    {
        private IKernel _kernel;

        public DIContainer()
        {
            _kernel = new StandardKernel();
        }

        public void Configure()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IDataProvider>().To<BaseDataProvider>().InSingletonScope();
            _kernel.Bind<Watcher.Watcher>().ToSelf();
        }

        public void ConfigureWindows()
        {
            // view models
            _kernel.Bind<ViewModelBase>().To<ViewModel_Main>().WhenInjectedInto<Window_Main>();
            _kernel.Bind<ViewModelBase>().To<ViewModel_Watches>().WhenInjectedInto<Window_Watches>();

            // windows
            _kernel.Bind<Window_Main>().ToSelf();
            _kernel.Bind<Window_Watches>().ToSelf();
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public void Dispose()
        {
            var dp = _kernel.Get<IDataProvider>();
            dp.Dispose();
            _kernel.Dispose();
        }
    }
}
