using System.ComponentModel;

namespace FileTag.MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            NamedCommands = new NamedCommands();
            RegisterCommands();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public INamedCommands NamedCommands { get; set; }

        protected virtual void RegisterCommands()
        {

        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RefreshProperty(string propName)
        {
            var prop = this.GetType().GetProperty(propName);
            var oldValue = prop.GetValue(this);
            prop.SetValue(this, null);
            prop.SetValue(this, oldValue);
        }
        
        public void PerformClose()
        {

        }
    }
}
