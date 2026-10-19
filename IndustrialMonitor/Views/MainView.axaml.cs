
using Avalonia;
using Avalonia.Controls;
using IndustrialMonitor.ViewModels;

namespace IndustrialMonitor.Views;

public partial class MainView : UserControl
{
    private const double CompactBreakpoint = 900;
    private const double CompactHeaderBreakpoint = 720;

    public MainView()
    {
        InitializeComponent();

        SizeChanged += MainView_SizeChanged;
    }

    private void MainView_SizeChanged(
        object? sender,
        SizeChangedEventArgs e)
    {
        double width = e.NewSize.Width;

        if (DataContext is MainViewModel vm)
        {
            vm.IsCompactLayout = width < CompactBreakpoint;
        }

        bool isCompactHeader = width < CompactHeaderBreakpoint;

        WideHeader.IsVisible = !isCompactHeader;
        CompactHeader.IsVisible = isCompactHeader;
    }
}
