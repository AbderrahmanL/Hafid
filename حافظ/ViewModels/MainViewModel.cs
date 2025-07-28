using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using حافظ.Models;
using حافظ.Services;
using حافظ.Helpers;
using حافظ.Views;
using static حافظ.App;

namespace حافظ.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly EntryStore _store;
    private readonly Action _navigateToAddEntryView;

    public ObservableCollection<MemorizationEntry> TodayEntries { get; } = new();

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
        _navigateToAddEntryView?.Invoke();
    }

    private void OnReschedule(MemorizationEntry entry)
    {
        _store.RescheduleEntry(entry);
        RefreshTodayEntries();
    }
}

