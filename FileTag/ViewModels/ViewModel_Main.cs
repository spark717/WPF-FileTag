using FileTag.Infrastacture;
using FileTag.Infrastacture.Watcher;
using FileTag.Models;
using FileTag.MVVM;
using FileTag.Views.Locators;
using FileTag.Views.Windows;
using System.Collections.Generic;
using System.Linq;

namespace FileTag.ViewModels
{
    public class ViewModel_Main : ViewModelBase
    {
        public ViewModel_Main(IDataProvider dataProvider, Watcher watcher)
        {
            _dataProvider = dataProvider;
            _watcher = watcher;
            _watcher.SyncComplete += (w) =>
            {
                RisePropertyChanged(nameof(Files));
            };

            // commands configuration
            ManageWatchFolders = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    //var w = new Window_Watches(_watcher);
                    WindowLocator.Default.OpenWindow<Window_Watches>();
                });

            AddTagToSelectedFile = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    if (SelectedFile == null) return;
                    if (string.IsNullOrEmpty(NewTagText)) return;

                    TryAddTagToFile(SelectedFile, NewTagText);
                    NewTagText = "";

                    RisePropertyChanged(nameof(SelectedFileTags));
                    RisePropertyChanged(nameof(Tags));
                });

            RemoveTag = new BaseCommand<string>()
                .Subscribe(message =>
                {
                    System.Windows.MessageBox.Show(message);
                });
        }

        // fields
        private Watcher _watcher;
        private IDataProvider _dataProvider;
        private DBFile _selectedFile;
        private string _newTagText;
        private ViewModel_Tag _selectedTag;

        // collections bindings
        public IEnumerable<DBFile> Files { get => LoadFiles(); }
        public IEnumerable<ViewModel_Tag> Tags { get => LoadTags(); }
        public IEnumerable<DBTag> SelectedFileTags { get => LoadSelectedFileTags(); }
        public IEnumerable<DBFile> SelectedTagFiles { get => LoadSelectedTagFiles(); }

        // selected binding
        public DBFile SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
            }
        }

        public ViewModel_Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                _selectedTag = value;
            }
        }

        // text bindings
        public string NewTagText
        {
            get => _newTagText;
            set
            {
                _newTagText = value;
                RisePropertyChanged(nameof(NewTagText));
            }
        }

        // commands
        public BaseCommand<object> ManageWatchFolders { get; set; }
        public BaseCommand<object> AddTagToSelectedFile { get; set; }
        public BaseCommand<string> RemoveTag { get; set; }

        private IEnumerable<DBFile> LoadFiles()
        {
            return _dataProvider.GetFiles().ToList();
        }

        private IEnumerable<ViewModel_Tag> LoadTags()
        {
            BaseCommand<ViewModel_Tag> testCommand = new BaseCommand<ViewModel_Tag>()
            .Subscribe(vm =>
            {
                _dataProvider.RemoveTag(vm.Data.Id);
                RisePropertyChanged(nameof(Tags));
            });

            return _dataProvider.GetTags().Select(t => new ViewModel_Tag(t) { RemoveCommand = testCommand });
        }

        private IEnumerable<DBTag> LoadSelectedFileTags()
        {
            return SelectedFile?.Tags.ToList();
        }

        private IEnumerable<DBFile> LoadSelectedTagFiles()
        {
            return SelectedTag?.Data.Files.ToList();
        }

        private void TryAddTagToFile(DBFile file, string tagName)
        {
            var tagId = _dataProvider.GetTag(tagName)?.Id;

            if (tagId == null)
            {
                var tag = new Tag()
                {
                    Name = tagName
                };

                _dataProvider.AddTags(tag);

                tagId = _dataProvider.GetTag(tagName).Id;
            }

            _dataProvider.AddRelation(file.Id, tagId.Value);
        }
    }
}