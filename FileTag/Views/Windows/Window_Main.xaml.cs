using FileTag.MVVM;

namespace FileTag.Views.Windows
{
    public partial class Window_Main : WindowBase
    {
        public Window_Main(ViewModelBase vm) : base(vm)
        {
           
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!NewTagFlyout.IsOpen)
                NewTagFlyout.IsOpen = true;
            else
                NewTagFlyout.IsOpen = false;
        }
    }
}
