using FileTag.Models;
using FileTag.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.ViewModels
{
    public class ViewModel_Tag : ViewModelBase, IBaseCommands<ViewModel_Tag>
    {
        public DBTag Data { get; set; }

        public BaseCommand<ViewModel_Tag> RemoveCommand { get ; set ; }

        public ViewModel_Tag(DBTag tag)
        {
            Data = tag;
        }
    }
}
