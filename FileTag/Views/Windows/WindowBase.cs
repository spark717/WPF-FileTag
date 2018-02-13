using FileTag.MVVM;
using MahApps.Metro.Controls;

namespace FileTag.Views.Windows
{
    public class WindowBase : MetroWindow
    {
        public WindowBase(ViewModelBase vm) : base()
        {
            this.GetType().GetMethod("InitializeComponent").Invoke(this, null);

            DataContext = vm;
        }
    }
}
