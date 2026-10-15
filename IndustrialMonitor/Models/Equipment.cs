
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace IndustrialMonitor.Models;

public partial class Equipment : ObservableObject
{
  private const int MaxHistorySamples = 30;

  public string Id { get; init; } = "";
  public string Name { get; init; } = "";

  [ObservableProperty]
  private EquipmentStatus _status;

  [ObservableProperty]
  private double _temperature;

  [ObservableProperty]
  private double _pressure;

  public ObservableCollection<TelemetrySample> History { get; } = new();

  public string StatusText =>
      Status.ToString().ToUpperInvariant();

  public string TemperatureText =>
      $"{Temperature:F1} °C";

  public string PressureText =>
      $"{Pressure:F1} bar";

  public double MaxTemperature => 120.0;
  public double MaxPressure => 16.0;

  public double TemperaturePercent =>
      Math.Clamp(Temperature / MaxTemperature * 100.0, 0, 100);

  public double PressurePercent =>
      Math.Clamp(Pressure / MaxPressure * 100.0, 0, 100);

  public void AddTelemetrySample(DateTime timestamp)
  {
    History.Add(new TelemetrySample(
        timestamp,
        Temperature,
        Pressure
    ));

    while (History.Count > MaxHistorySamples)
    {
      History.RemoveAt(0);
    }
  }

  partial void OnStatusChanged(EquipmentStatus value)
  {
    OnPropertyChanged(nameof(StatusText));
  }

  partial void OnTemperatureChanged(double value)
  {
    OnPropertyChanged(nameof(TemperatureText));
    OnPropertyChanged(nameof(TemperaturePercent));
  }

  partial void OnPressureChanged(double value)
  {
    OnPropertyChanged(nameof(PressureText));
    OnPropertyChanged(nameof(PressurePercent));
  }
}
