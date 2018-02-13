using FileTag.Models;
using FileTag.MVVM;
using System.Collections.Generic;

namespace FileTag.ViewModels
{
    public class ViewModel_File : ViewModelBase, IBaseCommands<ViewModel_File>
    {
        public DBFile Data { get; set; }

        public BaseCommand<ViewModel_File> RemoveCommand { get ; set ; }
    }
}