using FileTag.MVVM;
using MahApps.Metro.Controls;
using System;

namespace FileTag.Views.Windows
{
    public class WindowBase : MetroWindow
    {
        public WindowBase() : base()
        {

        }

        public WindowBase(ViewModelBase vm) : base()
        {
            this.GetType().GetMethod("InitializeComponent").Invoke(this, null);

            DataContext = vm;
        }

        protected void RegisterViewModelCommand(string name, Action<object> action)
        {
            ((ViewModelBase)DataContext).NamedCommands.Add(name, action);
        }
    }
}
