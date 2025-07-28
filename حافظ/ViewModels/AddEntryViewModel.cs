using System;
using System.Windows.Input;
using حافظ.Helpers;
using حافظ.Models;
using حافظ.Services;
namespace حافظ.ViewModels;

public class AddEntryViewModel : ViewModelBase
{
    private readonly EntryStore _store;
    private readonly Action _navigateBack;

    public string EntryText { get; set; } = "";
    public ICommand SaveCommand { get; }

    public AddEntryViewModel(EntryStore store, Action navigateBack)
    {
        _store = store;
        _navigateBack = navigateBack;
        SaveCommand = new RelayCommand(SaveEntry);
    }

    private void SaveEntry()
    {
        var entry = new MemorizationEntry
        {
            Text = EntryText,
            ScheduledDate = DateTime.Now.AddHours(2)
        };

        _store.AddEntry(entry);
        _navigateBack?.Invoke();
    }
}


