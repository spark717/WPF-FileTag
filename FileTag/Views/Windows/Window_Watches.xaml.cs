using FileTag.Infrastacture.Watcher;
using FileTag.MVVM;
using FileTag.ViewModels;
using MahApps.Metro.Controls;

namespace FileTag.Views.Windows
{
    public partial class Window_Watches : WindowBase
    {
        public Window_Watches(ViewModelBase vm) : base(vm)
        {
            //InitializeComponent();

            vm.Closing += () =>
            {
                Close();
            };
        }
    }
}
