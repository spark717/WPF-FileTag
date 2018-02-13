using FileTag.Infrastacture;
using FileTag.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
