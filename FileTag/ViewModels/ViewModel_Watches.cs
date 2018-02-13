using FileTag.Infrastacture.Watcher;
using FileTag.Models.FS;
using FileTag.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace FileTag.ViewModels
{
    public class ViewModel_Watches : ViewModelBase
    {
        private Watcher _watcher;

        public ObservableCollection<FSObject> WatchDirs { get; set; }
        public List<FSObject> CurrentDirItems { get; set; }
        public LinkedList<FSObject> CurrentDirPath { get; set; }
        public ICommand GoToConcreteDir { get; set; }
        public ICommand AddDirToWatch { get; set; }
        public ICommand GoToParentDir { get; set; }
        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        public ViewModel_Watches(Watcher watcher)
        {
            _watcher = watcher;
            WatchDirs = new ObservableCollection<FSObject>();

            GoToConcreteDir = new BaseCommand<FSObject>()
                .Subscribe(dir =>
                {
                    GoToConcrete(dir.FullPath);
                })
                .While(dir =>
                {
                    return dir.IsDir();
                });

            AddDirToWatch = new BaseCommand<FSObject>()
                .Subscribe(dir =>
                {
                    AddToWatch(dir);
                })
                .While(item =>
                {
                    return !IsWatching(item) && item.IsDir();
                })
                .ReactOnSelf();

            Save = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    PerformClose();
                });

            Cancel = new BaseCommand<object>()
                .Subscribe(() =>
                {
                    PerformClose(cancel: true);
                });

            GoToConcrete("root");
        }

        private void AddToWatch(FSObject dir)
        {
            if (!dir.IsDir())
                return;

            if (IsWatching(dir))
                return;

            WatchDirs.Add(dir);
        }

        private void RemoveFromWatch(FSObject dir)
        {
            
        }

        private bool IsWatching(FSObject dir)
        {
            return WatchDirs.Any(i => i.FullPath == dir.FullPath);
        }

        private List<FSObject> GetDirItems(string path)
        {
            List<FSObject> items = new List<FSObject>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var dir in dirInfo.EnumerateDirectories())
            {
                items.Add(new FSObject(dir));
            }

            foreach (var file in dirInfo.EnumerateFiles())
            {
                items.Add(new FSObject(file));
            }

            return items;
        }

        private List<FSObject> GetDrives()
        {
            return DriveInfo.GetDrives().Select(di => new FSObject(di)).ToList();
        }

        private void MakePath(string path)
        {
            CurrentDirPath = new LinkedList<FSObject>();
            var items = CurrentDirPath;

            if (path != "root")
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                while (dir != null)
                {
                    items.AddFirst(new FSObject(dir));

                    dir = dir.Parent;
                }
            }

            items.AddFirst(FSObject.Root());
        }

        private void GoToConcrete(string path)
        {
            if (path == "root")
            {
                CurrentDirItems = GetDrives();
                MakePath(path);
            }
            else if (Directory.Exists(path))
            {
                CurrentDirItems = GetDirItems(path);
                MakePath(path);
            }
            else
                return;

            RisePropertyChanged(nameof(CurrentDirPath));
            RisePropertyChanged(nameof(CurrentDirItems));
        }

        protected override void OnClose(bool cancel)
        {
            if (!cancel)
            {
                _watcher.Dirs = WatchDirs.Select(i => i.FullPath).ToList();
                _watcher.Scan(syncAfterScan: true);
            }
        }
    }
}