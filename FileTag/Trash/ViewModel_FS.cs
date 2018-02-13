using FileTag.Models.FS;
using FileTag.MVVM;
using FileTag.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FileTag.ViewModels
{
    [Obsolete]
    public class ViewModel_FS : ViewModelBase
    {
        //private Dictionary<string, FSObject> _watchDirs;

        //public ObservableCollection<FSObject> DirItems { get; set; }
        //public ObservableCollection<FSObject> WatchDirs { get; set; }
        //public ObservableCollection<FSObject> DirPathItems { get; set; }
        //public Command<FSObject> PlusAction { get; set; }
        //public Command<FSObject> MinusAction { get; set; }
        //public Command<FSObject> GoToDir { get; set; }
        //public Command<object> GoToParentDir { get; set; }
        //public Command<FSObject> GoToConcreteDir { get; set; }
        //public Command<object> Save { get; set; }
        //public Command<object> Cancel { get; set; }

        //public ViewModel_FS()
        //{
        //    var items = DriveInfo.GetDrives().Select(i => i.ToString()).ToList();

        //    _watchDirs = new Dictionary<string, FSObject>();

        //    DirItems = new ObservableCollection<FSObject>();
        //    WatchDirs = new ObservableCollection<FSObject>();
        //    DirPathItems = new ObservableCollection<FSObject>();

        //    LoadDir("root");

        //    PlusAction = Command<FSObject>.Create()
        //        .Action(fsobj =>
        //        {
        //            AddWatchDir(fsobj);
        //            PlusAction.RiseCanExecuteChanged();
        //        })
        //        .While(fsobj =>
        //        {
        //            return !IsWatching(fsobj.FullPath) && fsobj.IsDir();
        //        });
        //        //.Configure(command =>
        //        //{
        //        //    WatchDirs.CollectionChanged += (sender, args) =>
        //        //    {
        //        //        command.RiseCanExecuteChanged();
        //        //    };
        //        //});

        //    MinusAction = Command<FSObject>.Create()
        //        .Action(fsobj =>
        //        {
        //            RemoveWatchDir(fsobj);
        //            PlusAction.RiseCanExecuteChanged();
        //        });

        //    GoToDir = Command<FSObject>.Create()
        //        .Action(fsobj =>
        //        {
        //            LoadDir(fsobj.FullPath);
        //            DirPathItems.Add(fsobj);
        //            GoToParentDir.RiseCanExecuteChanged();
        //        })
        //        .While(fsobj =>
        //        {
        //            return fsobj.IsDir();
        //        });

        //    GoToParentDir = Command<object>.Create()
        //        .Action(arg =>
        //        {
        //            if (DirPathItems.Count == 0)
        //                return;

        //            DirPathItems.Remove(DirPathItems.Last());

        //            if (DirPathItems.Count == 0)
        //                LoadDir("root");
        //            else
        //                LoadDir(DirPathItems.Last().FullPath);

        //            GoToParentDir.RiseCanExecuteChanged();
        //        })
        //        .While(arg =>
        //        {
        //            return DirPathItems.Count > 0;
        //        });

        //    GoToConcreteDir = Command<FSObject>.Create()
        //        .Action(fsobj =>
        //        {
        //            int itemIndex = DirPathItems.IndexOf(fsobj);

        //            for (int i = DirPathItems.Count - 1; i > itemIndex ; i--)
        //            {
        //                DirPathItems.RemoveAt(i);
        //            }

        //            LoadDir(DirPathItems.Last().FullPath);
        //        });

        //    Save = Command<object>.Create()
        //        .Action(arg =>
        //        {
        //            NavigationService.GetService().GoBack(WatchDirs.Select(i => i.FullPath));
        //        });

        //    Cancel = Command<object>.Create()
        //        .Action(arg =>
        //        {
        //            NavigationService.GetService().GoBack();
        //        });
        //}

        //private void LoadDir(string path)
        //{
        //    //if (path == "root")
        //    //{
        //    //    DirItems.Clear();

        //    //    foreach (var drive in DriveInfo.GetDrives())
        //    //    {
        //    //        DirItems.Add(new FSObject(type: FSObjectType.Dir, name: drive.Name, fullPath: drive.Name));
        //    //    }
        //    //}
        //    //else if (Directory.Exists(path))
        //    //{
        //    //    DirItems.Clear();

        //    //    var dirInfo = new DirectoryInfo(path);

        //    //    foreach (var dir in dirInfo.EnumerateDirectories())
        //    //    {
        //    //        DirItems.Add(new FSObject(type: FSObjectType.Dir, name: dir.Name, fullPath: dir.FullName));
        //    //    }

        //    //    foreach (var file in dirInfo.EnumerateFiles())
        //    //    {
        //    //        DirItems.Add(new FSObject(type: FSObjectType.File, name: file.Name, fullPath: file.FullName));
        //    //    }
        //    //}
        //}

        //private void AddWatchDir(FSObject dir)
        //{
        //    if (_watchDirs.ContainsKey(dir.FullPath))
        //        return;

        //    _watchDirs.Add(dir.FullPath, dir);
        //    WatchDirs.Add(dir);
        //}

        //private void RemoveWatchDir(FSObject dir)
        //{
        //    if (!_watchDirs.ContainsKey(dir.FullPath))
        //        return;

        //    _watchDirs.Remove(dir.FullPath);
        //    WatchDirs.Remove(dir);
        //}

        //private bool IsWatching(string path)
        //{
        //    if (_watchDirs.ContainsKey(path))
        //        return true;

        //    return false;
        //}
    }
}
