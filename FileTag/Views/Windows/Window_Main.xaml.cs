using FileTag.MVVM;

namespace FileTag.Views.Windows
{
    public partial class Window_Main : WindowBase
    {
        //public Window_Main()
        //{
        //    InitializeComponent();
        //}
        
        public Window_Main(ViewModelBase vm) : base(vm)
        {
           // InitializeComponent();
        }
    }
}
