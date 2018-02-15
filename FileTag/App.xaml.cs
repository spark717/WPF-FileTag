using FileTag.Infrastacture;
using FileTag.Views.Locators;
using FileTag.Views.Windows;
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
