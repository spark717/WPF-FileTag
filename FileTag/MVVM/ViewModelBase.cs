using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action Closing;

        public void RisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Ocures when page going to history, and opened new page
        /// </summary>
        public virtual void OnSleep()
        {
        }

        /// <summary>
        /// Ocures when page returns from history, by back action
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnAwake(object args)
        {
        }

        protected virtual void OnClose(bool cancel = false)
        {
        }

        public virtual void PerformClose(bool cancel = false)
        {
            OnClose();
            Closing?.Invoke();
        }
    }
}
