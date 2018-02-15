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

        public static ViewModel_File Create(DBFile file)
        {
            return new ViewModel_File(file);
        }

        public List<ViewModel_Tag> Tags
        {
            get
            {
                _tags.SmartFill(Data.Tags, ViewModel_Tag.Create);
                return _tags;
            }
        }
    }
}