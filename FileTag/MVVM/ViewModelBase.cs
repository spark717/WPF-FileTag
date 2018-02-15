using System;
using System.ComponentModel;

namespace FileTag.MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action Closing;

        protected void RisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void OnPropertyChanged(string prop, Action action)
        {
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == prop)
                    action();
            };
        }

        protected void RefreshProperty(string propName)
        {
            var prop = this.GetType().GetProperty(propName);
            var oldValue = prop.GetValue(this);
            prop.SetValue(this, null);
            prop.SetValue(this, oldValue);
        }
        
    }
}
