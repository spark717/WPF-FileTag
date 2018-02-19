using FileTag.Help;
using FileTag.Models;
using FileTag.MVVM;
using System.Collections.Generic;

namespace FileTag.ViewModels
{
    public class ViewModel_File : BaseContainer<DBFile>
    {
        public ViewModel_File(DBFile data) : base(data)
        {
            _tags = new List<ViewModel_Tag>();
        }

        private List<ViewModel_Tag> _tags;

        public List<ViewModel_Tag> Tags
        {
            get
            {
                _tags.SmartFill(Data.Tags, TagCreationStrategy);
                return _tags;
            }
        }

        public BaseCommand<ViewModel_Tag> RemoveTagCommand { get; set; }

        public static ViewModel_File Create(DBFile file)
        {
            return new ViewModel_File(file);
        }

        private ViewModel_Tag TagCreationStrategy(DBTag tag)
        {
            return new ViewModel_Tag(tag) { RemoveCommand = RemoveTagCommand };
        }
    }
}