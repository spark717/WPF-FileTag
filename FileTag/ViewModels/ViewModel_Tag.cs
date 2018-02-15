using FileTag.Models;
using FileTag.MVVM;
using System.Collections.Generic;

namespace FileTag.ViewModels
{
    public class ViewModel_Tag : BaseContainer<DBTag>
    {
        public ViewModel_Tag(DBTag tag) : base(tag)
        {
            _files = new List<ViewModel_File>();
        }

        private List<ViewModel_File> _files;


        public static ViewModel_Tag Create(DBTag tag)
        {
            return new ViewModel_Tag(tag);
        }

        public BaseCommand<ViewModel_Tag> RemoveCommand { get; set; }

        public List<ViewModel_File> Files
        {
            get
            {
                _files.SmartFill(Data.Files, ViewModel_File.Create);
                return _files;
            }
        }
    }
}
