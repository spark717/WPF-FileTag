using FileTag.Infrastacture;
using FileTag.Views.Locators;
using FileTag.Views.Windows;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FileTag
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using(var diContainer = new DIContainer())
            {
                diContainer.Configure();
                diContainer.ConfigureWindows();

                var wl = new WindowLocator(diContainer);

                wl.OpenWindow<Window_Main>();
            }
        }
    }
}
