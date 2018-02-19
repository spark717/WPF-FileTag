using FileTag.Infrastacture;
using FileTag.Views.Windows;

namespace FileTag.Views.Locators
{
    public class WindowLocator
    {
        private DIContainer _diContainer;

        public static WindowLocator Default { get; private set; }

        public WindowLocator(DIContainer diContainer)
        {
            _diContainer = diContainer;
            Default = this;
        }

        public void OpenWindow<T>() where T: WindowBase
        {
            T window = _diContainer.Get<T>();
            window.ShowDialog();
        }
    }
}
