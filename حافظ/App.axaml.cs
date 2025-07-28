using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using حافظ.Services;
using حافظ.ViewModels;
using حافظ.Views;

namespace حافظ;

public partial class App : Application
{
    private EntryStore _store; // Move store to class level

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _store = new EntryStore(); // Initialize the store
            
            Window mainWindow = new Window
            {
                MinWidth = 400,
                MinHeight = 600,
                Width = 400,
                Height = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Initial navigation
            NavigateToMainView(mainWindow);

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void NavigateToAddEntryView(Window window)
    {
        try
        {
            window.Content = new AddEntryView
            {
                DataContext = new AddEntryViewModel(_store, () => NavigateToMainView(window))
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation to AddEntryView failed: {ex.Message}");
            // Fallback to main view if error occurs
            NavigateToMainView(window);
        }
    }

    private void NavigateToMainView(Window window)
    {
        try
        {
            window.Content = new MainView
            {
                DataContext = new MainViewModel(_store, () => NavigateToAddEntryView(window))
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation to MainView failed: {ex.Message}");
            // Show error view or terminate if even main view fails
            window.Content = new TextBlock { Text = "Application Error" };
        }
    }
}
