using FileTag.Infrastacture;
using FileTag.ViewModels;
using System;

namespace FileTag.Views.Pages
{
    [Obsolete]
    public class PageFactory
    {
        //private static BasePage GetPage(BasePage page, ViewModelBase viewModel)
        //{
        //    page.DataContext = viewModel;
        //    return page;
        //}

        //public static T GetPage<T>() where T: BasePage, new()
        //{
        //    if (typeof(T) == typeof(Page_Files))
        //        return (T)GetPage(new Page_Files(), new ViewModel_Files(new BaseDataProvider()));

        //    if (typeof(T) == typeof(Page_FS))
        //        return (T)GetPage(new Page_FS(), new ViewModel_FS());

        //    return new T();
        //}
    }
}