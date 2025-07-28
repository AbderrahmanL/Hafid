using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using حافظ.Services;
using حافظ.ViewModels;
using حافظ.Views;

namespace حافظ;

public partial class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        var store = new EntryStore();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            void ShowMainView()
            {
                desktop.MainWindow.Content = new MainView
                {
                    DataContext = new MainViewModel(store, ShowAddEntryView)
                };
            }

            void ShowAddEntryView()
            {
                desktop.MainWindow.Content = new AddEntryView
                {
                    DataContext = new AddEntryViewModel(store, ShowMainView)
                };
            }

            desktop.MainWindow = new Window();
            ShowMainView();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime mobile)
        {
            void ShowMainView()
            {
                mobile.MainView = new MainView
                {
                    DataContext = new MainViewModel(store, ShowAddEntryView)
                };
            }

            void ShowAddEntryView()
            {
                mobile.MainView = new AddEntryView
                {
                    DataContext = new AddEntryViewModel(store, ShowMainView)
                };
            }

            ShowMainView();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
