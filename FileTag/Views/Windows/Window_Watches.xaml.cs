using FileTag.MVVM;

namespace FileTag.Views.Windows
{
    public partial class Window_Watches : WindowBase
    {
        public Window_Watches(ViewModelBase vm) : base(vm)
        {
            RegisterViewModelCommand("close", param =>
            {
                Close();
            });
        }
    }
}
