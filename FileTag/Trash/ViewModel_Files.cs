using FileTag.Infrastacture;
using FileTag.Models;
using FileTag.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FileTag.ViewModels
{
    public class ViewModel_Files : ViewModelBase
    {
        //private IDataProvider _dataProvider;
        //private File _selectedFile;

        //public ObservableCollection<File> Files { get; set; }
        //public ObservableCollection<Tag> SelectedFileTags { get; set; }
        //public File SelectedFile
        //{
        //    get => _selectedFile;
        //    set
        //    {
        //        _selectedFile = value;
        //        OnFileSelection.Execute(value);
        //    }
        //}
        //public ICommand OnFileSelection { get; set; }

        //public ViewModel_Files(IDataProvider dataProvider)
        //{
        //    this._dataProvider = dataProvider;

        //    Files = new ObservableCollection<File>(_dataProvider.GetFiles());
        //    SelectedFileTags = new ObservableCollection<Tag>();

        //    OnFileSelection = Command<File>.Create()
        //        .Action(file =>
        //        {
        //            SelectedFileTags = new ObservableCollection<Tag>(_dataProvider.GetTags(file));
        //            RisePropertyChanged(nameof(SelectedFileTags));
        //        });
        //}
    }
}
