using Avalonia;
using Avalonia.Controls;

namespace IndustrialMonitor.Views;

public partial class DashboardView : UserControl
{
  private const double WideBreakpoint = 900;
  private const double MediumBreakpoint = 500;

  public DashboardView()
  {
    InitializeComponent();

    SizeChanged += DashboardView_SizeChanged;
  }

  private void DashboardView_SizeChanged(
      object? sender,
      SizeChangedEventArgs e)
  {
    double width = e.NewSize.Width;

    // KPI: 4, 2 или 1 колонка
    int columns = width switch
    {
      >= WideBreakpoint => 4,
      >= MediumBreakpoint => 2,
      _ => 1
    };

    KpiGrid.Columns = columns;

    // Simulation: горизонтально или вертикально
    bool isNarrow = width < 500;

    SimulationGrid.ColumnDefinitions =
        new ColumnDefinitions(
            isNarrow ? "*" : "*,Auto"
        );

    SimulationGrid.RowDefinitions =
        new RowDefinitions(
            isNarrow ? "Auto,Auto" : "Auto"
        );

    Grid.SetRow(SimulationButton, isNarrow ? 1 : 0);
    Grid.SetColumn(SimulationButton, isNarrow ? 0 : 1);

    SimulationButton.HorizontalAlignment =
        isNarrow
            ? Avalonia.Layout.HorizontalAlignment.Stretch
            : Avalonia.Layout.HorizontalAlignment.Right;
  }

}