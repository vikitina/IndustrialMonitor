
using System;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using IndustrialMonitor.Models;

namespace IndustrialMonitor.Views;

public partial class TemperatureChartView : UserControl
{
  private Equipment? _equipment;

  public TemperatureChartView()
  {
    InitializeComponent();

    DataContextChanged += OnDataContextChanged;
    DetachedFromVisualTree += (_, _) => Unsubscribe();
    AttachedToVisualTree += (_, _) => Subscribe();
  }

  private void OnDataContextChanged(object? sender, EventArgs e)
  {
    Unsubscribe();

    _equipment = DataContext as Equipment;

    Subscribe();
    DrawChart();
  }

  private void Subscribe()
  {
    if (_equipment is not null)
    {
      _equipment.History.CollectionChanged -= OnHistoryChanged;
      _equipment.History.CollectionChanged += OnHistoryChanged;
    }
  }

  private void Unsubscribe()
  {
    if (_equipment is not null)
    {
      _equipment.History.CollectionChanged -= OnHistoryChanged;
    }
  }

  private void OnHistoryChanged(
      object? sender,
      NotifyCollectionChangedEventArgs e)
  {
    DrawChart();
  }

  private void ChartCanvas_SizeChanged(
      object? sender,
      SizeChangedEventArgs e)
  {
    DrawChart();
  }

  private void DrawChart()
  {
    if (_equipment is null)
    {
      TemperatureLine.Points = new Points();
      return;
    }

    var history = _equipment.History;

    if (history.Count == 0)
    {
      TemperatureLine.Points = new Points();
      return;
    }

    double width = ChartCanvas.Bounds.Width;
    double height = ChartCanvas.Bounds.Height;

    if (width <= 0 || height <= 0)
      return;

    const double minTemperature = 0;
    const double maxTemperature = 120;

    var points = new Points();

    for (int i = 0; i < history.Count; i++)
    {
      double x = history.Count == 1
          ? width / 2
          : i * width / (history.Count - 1);

      double normalizedTemperature =
          (history[i].Temperature - minTemperature) /
          (maxTemperature - minTemperature);

      double y = height -
          normalizedTemperature * height;

      points.Add(new Point(x, y));
    }

    TemperatureLine.Points = points;
  }
}
