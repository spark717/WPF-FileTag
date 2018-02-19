using FileTag.Help;
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
        public ViewModel_Main(IDataProvider dataProvider, Watcher watcher) : base()
        {
            _dataProvider = dataProvider;
            _watcher = watcher;
            _watcher.SyncComplete += w =>
            {
                //RisePropertyChanged(nameof(Files));
            };

            Tags = new List<ViewModel_Tag>();
            Files = new List<ViewModel_File>();

            //InitCommands();

            //ConfigureEvents();

            SelectedTabIndex = -1;
            SelectedTabIndex = 0;
        }

        // fields
        private Watcher _watcher;
        private IDataProvider _dataProvider;

        // collections bindings
        public List<ViewModel_File> Files { get; set; }
        public List<ViewModel_Tag> Tags { get; set; }
        public List<ViewModel_Tag> SelectedFileTags { get; set; }
        public List<ViewModel_File> SelectedTagFiles { get; set; }

        // selected binding
        public ViewModel_File SelectedFile { get; set; }
        public ViewModel_Tag SelectedTag { get; set; }
        public int SelectedTabIndex { get; set; }

        // text bindings
        public string AddTagName { get; set; }
        public string NewTagName { get; set; }
        public string NewTagDescription { get; set; }

        // commands
        public BaseCommand<object> ManageWatchFoldersCommand { get; set; }
        public BaseCommand<object> NewTagCommand { get; set; }
        public BaseCommand<object> AddTagToSelectedFileCommand { get; set; }
        public BaseCommand<ViewModel_Tag> RemoveTagCommand { get; set; }

        protected override void RegisterCommands()
        {
            ManageWatchFoldersCommand = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    WindowLocator.Default.OpenWindow<Window_Watches>();
                });

            NewTagCommand = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    if (string.IsNullOrEmpty(NewTagName) || string.IsNullOrEmpty(NewTagDescription)) return;

                    var newTag = new Tag()
                    {
                        Name = NewTagName,
                        Description = NewTagDescription
                    };

                    _dataProvider.NewTag(newTag);

                    NewTagName = null;
                    NewTagDescription = null;

                    if (SelectedTabIndex == 1)
                        UpdateTags();
                });

            AddTagToSelectedFileCommand = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    if (SelectedFile == null) return;
                    if (string.IsNullOrEmpty(AddTagName)) return;

                    Tag tag = new Tag() { Name = AddTagName };
                    DBFile file = SelectedFile.Data;

                    _dataProvider.TryAddTag(tag, file);

                    AddTagName = "";

                    RefreshProperty(nameof(SelectedFile));
                });

            RemoveTagCommand = new BaseCommand<ViewModel_Tag>()
                .Subscribe(vm =>
                {
                    _dataProvider.RemoveTag(vm.Data);
                    UpdateTags();
                });
        }

        private void UpdateFiles()
        {
            var removeTagCommand = new BaseCommand<ViewModel_Tag>()
                .Subscribe(t =>
                {
                    var file = SelectedFile.Data;
                    var tag = t.Data;

                    _dataProvider.RemoveRelation(file, tag);
                    RefreshProperty(nameof(SelectedFile));
                });

            ViewModel_File creationStrategy(DBFile file)
            {
                return new ViewModel_File(file) { RemoveTagCommand = removeTagCommand };
            }

            Files = Files.SmartFill(_dataProvider.GetFiles(), creationStrategy, false);
            RefreshProperty(nameof(SelectedFile));
        }

        private void UpdateTags()
        {
            // how remove tag from db
            var removeCommand = new BaseCommand<ViewModel_Tag>()
                .Subscribe(vm =>
                {
                    _dataProvider.RemoveTag(vm.Data);
                    UpdateTags();
                });

            // how create new vm
            ViewModel_Tag creationStrategy(DBTag tag)
            {
                return new ViewModel_Tag(tag) { RemoveCommand = removeCommand };
            }

            Tags = Tags.SmartFill(_dataProvider.GetTags(), creationStrategy, false);
            RefreshProperty(nameof(SelectedTag));
        }

        // FODY auto notifyers
        private void OnSelectedFileChanged()
        {
            SelectedFileTags = SelectedFile?.Data.Tags.Select(t => new ViewModel_Tag(t)).ToList();
        }

        private void OnSelectedTabIndexChanged()
        {
            if (SelectedTabIndex == 0)
            {
                UpdateFiles();
            }
            else if (SelectedTabIndex == 1)
            {
                UpdateTags();
            }
        }
    }
}