using FileTag.Infrastacture;
using FileTag.Infrastacture.Watcher;
using FileTag.Models;
using FileTag.MVVM;
using FileTag.Views.Locators;
using FileTag.Views.Windows;
using System;
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
                //RisePropertyChanged(nameof(Files));
            };

            Tags = new List<ViewModel_Tag>();
            Files = new List<ViewModel_File>();

            InitCommands();

            ConfigureEvents();

            SelectedTabIndex = -1;
            SelectedTabIndex = 0;
        }

        // fields
        private Watcher _watcher;
        private IDataProvider _dataProvider;

        // collections bindings
        public List<ViewModel_File> Files { get; set; }
        public List<ViewModel_Tag> Tags { get; set; }

        // selected binding
        public ViewModel_File SelectedFile { get; set; }
        public ViewModel_Tag SelectedTag { get; set; }
        public int SelectedTabIndex { get; set; }

        // text bindings
        public string NewTagText { get; set; }
        public int SelectedFile_Tags { get; set; }
        // commands
        public BaseCommand<object> ManageWatchFoldersCommand { get; set; }
        public BaseCommand<object> AddTagToSelectedFileCommand { get; set; }
        public BaseCommand<ViewModel_Tag> RemoveTagCommand { get; set; }

        private void InitCommands()
        {
            ManageWatchFoldersCommand = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    WindowLocator.Default.OpenWindow<Window_Watches>();
                });

            AddTagToSelectedFileCommand = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    if (SelectedFile == null) return;
                    if (string.IsNullOrEmpty(NewTagText)) return;

                    Tag tag = new Tag() { Name = NewTagText };
                    DBFile file = SelectedFile.Data;

                    _dataProvider.TryAddTag(tag, file);

                    NewTagText = "";

                    RefreshProperty(nameof(SelectedFile));
                });

            RemoveTagCommand = new BaseCommand<ViewModel_Tag>()
                .Subscribe(vm =>
                {
                    _dataProvider.RemoveTag(vm.Data);
                    UpdateTags();
                });
        }

        private void ConfigureEvents()
        {
            OnPropertyChanged(nameof(SelectedTabIndex), () =>
            {
                if (SelectedTabIndex == 0)
                {
                    UpdateFiles();
                }
                else if (SelectedTabIndex == 1)
                {
                    UpdateTags();
                }
            });
        }

        private void UpdateFiles()
        {
            Files = Files.SmartFill(_dataProvider.GetFiles(), ViewModel_File.Create, false);
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
            Func<DBTag, ViewModel_Tag> creationStrategy = tag =>
            {
                return new ViewModel_Tag(tag) { RemoveCommand = removeCommand };
            };

            Tags = Tags.SmartFill(_dataProvider.GetTags(), creationStrategy, false);
            RefreshProperty(nameof(SelectedTag));
        }
    }
}