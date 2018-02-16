using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.MVVM
{
    public class SmartObsCollection<T> : ObservableCollection<T>
    {
        public SmartObsCollection()
        {
            PropertyChanged += (s, e) =>
            {

            };

            CollectionChanged += (s, e) =>
            {

            };
        }
    }
}
