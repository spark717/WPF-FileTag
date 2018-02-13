using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.MVVM
{
    public interface IBaseCommands<T>
    {
        BaseCommand<T> RemoveCommand { get; set; }
    }
}
