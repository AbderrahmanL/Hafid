using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace حافظ.Views;

public partial class AddEntryView : UserControl
{
    public AddEntryView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}