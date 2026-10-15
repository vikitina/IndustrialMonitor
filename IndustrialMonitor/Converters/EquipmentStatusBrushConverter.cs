
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using IndustrialMonitor.Models;

namespace IndustrialMonitor.Converters;

public class EquipmentStatusBrushConverter : IValueConverter
{
  public object Convert(
      object? value,
      Type targetType,
      object? parameter,
      CultureInfo culture)
  {
    return value switch
    {
      EquipmentStatus.Running =>
          new SolidColorBrush(Color.Parse("#22C55E")),

      EquipmentStatus.Warning =>
          new SolidColorBrush(Color.Parse("#F59E0B")),

      EquipmentStatus.Critical =>
          new SolidColorBrush(Color.Parse("#F43F5E")),

      EquipmentStatus.Stopped =>
          new SolidColorBrush(Color.Parse("#94A3B8")),

      _ => Brushes.Gray
    };
  }

  public object ConvertBack(
      object? value,
      Type targetType,
      object? parameter,
      CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
