using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using حافظ.Models;
using حافظ.Services;
using حافظ.Helpers;
using حافظ.Views;

namespace حافظ.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly EntryStore _store;
    private readonly Action _navigateToAddEntryView;

    public ObservableCollection<MemorizationEntry> TodayEntries { get; set; } = new();

    public ICommand AddEntryCommand { get; }
    public ICommand RescheduleCommand { get; }

    public MainViewModel(EntryStore store, Action navigateToAddEntryView)
    {
        _store = store;
        _navigateToAddEntryView = navigateToAddEntryView;

        _store.Load();
        RefreshTodayEntries();

        AddEntryCommand = new RelayCommand(OnAddEntry);
        RescheduleCommand = new RelayCommand<MemorizationEntry>(OnReschedule);
    }

    private void RefreshTodayEntries()
    {
        TodayEntries.Clear();
        foreach (var e in _store.GetEntriesDueToday())
            TodayEntries.Add(e);
    }

    private void OnAddEntry()
    {
        var addEntryView = new AddEntryView
        {
            DataContext = new AddEntryViewModel(_store, NavigateBackToMainView)
        };
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Content = addEntryView;
        }
    }

    private void NavigateBackToMainView()
    {
        var mainView = new MainView
        {
            DataContext = new MainViewModel(_store, OnAddEntry)
        };
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Content = mainView;
        }
    }


    private void OnReschedule(MemorizationEntry entry)
    {
        _store.RescheduleEntry(entry);
        RefreshTodayEntries();
    }
}

