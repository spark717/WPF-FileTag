using FileTag.Views.Pages;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FileTag.Views
{
    [Obsolete]
    public class NavigationService
    {
        //public NavigationService()
        //{
        //    _pageHistory = new LinkedList<BasePage>();
        //}

        //private static NavigationService _instance = new NavigationService();

        //private LinkedList<BasePage> _pageHistory;

        //private BasePage CurrentPage { get => _pageHistory.Last?.Value; }
        //public Frame MainFrame { get; set; }
        //public event Action<BasePage> CurrentPageChanged;

        //public static NavigationService GetService()
        //{
        //    if (_instance == null)
        //        _instance = new NavigationService();

        //    return _instance;
        //}
        
        //public void GoToPage<T>() where T: BasePage, new()
        //{
        //    CurrentPage?.ViewModel.OnSleep();

        //    var page = PageFactory.GetPage<T>();
            
        //    _pageHistory.AddLast(page);

        //    ShowCurrentPage();

        //    OnCurrentPageChanged();
        //}

        //public void GoBack(object args = null)
        //{
        //    if (_pageHistory.Count < 2)
        //        return;

        //    _pageHistory.RemoveLast();

        //    ShowCurrentPage();

        //    CurrentPage.ViewModel.OnAwake(args);

        //    OnCurrentPageChanged();
        //}

        //public bool HistoryContainsPage<T>() where T: BasePage
        //{
        //    foreach (var page in _pageHistory)
        //    {
        //        if (page is T)
        //            return true;
        //    }

        //    return false;
        //}

        //private void OnCurrentPageChanged()
        //{
        //    CurrentPageChanged?.Invoke(CurrentPage);
        //}

        //private void ShowCurrentPage()
        //{
        //    if (MainFrame == null)
        //        throw new NullReferenceException(nameof(MainFrame));

        //    MainFrame.Content = CurrentPage;
        //}
    }
}