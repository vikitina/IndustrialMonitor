
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Avalonia;
using Avalonia.Styling;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IndustrialMonitor.Models;
using IndustrialMonitor.Services;

namespace IndustrialMonitor.ViewModels;

public partial class MainViewModel : ViewModelBase, IDisposable
{
  private readonly EquipmentSimulationService _simulationService;
  private readonly DispatcherTimer _simulationTimer;

  public ObservableCollection<Equipment> EquipmentItems { get; }

  [ObservableProperty]
  private Equipment? _selectedEquipment;

  [ObservableProperty]
  private bool _isDarkTheme = true;

  [ObservableProperty]
  private bool _isSimulationRunning;

  [ObservableProperty]
  private DateTime _lastUpdated = DateTime.Now;

  public int TotalEquipment => EquipmentItems.Count;

  public int RunningEquipment =>
      EquipmentItems.Count(e => e.Status == EquipmentStatus.Running);

  public int WarningEquipment =>
      EquipmentItems.Count(e => e.Status == EquipmentStatus.Warning);

  public int CriticalEquipment =>
      EquipmentItems.Count(e => e.Status == EquipmentStatus.Critical);

  public int StoppedEquipment =>
      EquipmentItems.Count(e => e.Status == EquipmentStatus.Stopped);

  public string ThemeButtonText =>
      IsDarkTheme ? "☀ Light mode" : "☾ Dark mode";

  public string SimulationButtonText =>
      IsSimulationRunning ? "Pause simulation" : "Start simulation";

  public string SimulationStatusText =>
      IsSimulationRunning ? "● LIVE SIMULATION" : "● SIMULATION PAUSED";

  public string LastUpdatedText =>
      $"Last updated: {LastUpdated:HH:mm:ss}";

  public MainViewModel()
  {
    var equipmentService = new EquipmentService();
    _simulationService = new EquipmentSimulationService();

    EquipmentItems = new ObservableCollection<Equipment>(
        equipmentService.GetEquipment()
    );

    foreach (var equipment in EquipmentItems)
    {
      equipment.PropertyChanged += Equipment_PropertyChanged;
    }

    EquipmentItems.CollectionChanged += EquipmentItems_CollectionChanged;

    SelectedEquipment = EquipmentItems.FirstOrDefault();

    _simulationTimer = new DispatcherTimer
    {
      Interval = TimeSpan.FromSeconds(2)
    };

    _simulationTimer.Tick += SimulationTimer_Tick;
  }

  private void SimulationTimer_Tick(object? sender, EventArgs e)
  {
    _simulationService.UpdateEquipment(EquipmentItems);
    LastUpdated = DateTime.Now;
  }

  [RelayCommand]
  private void ToggleSimulation()
  {
    if (IsSimulationRunning)
    {
      _simulationTimer.Stop();
      IsSimulationRunning = false;
    }
    else
    {
      _simulationTimer.Start();
      IsSimulationRunning = true;
    }
  }

  partial void OnIsSimulationRunningChanged(bool value)
  {
    OnPropertyChanged(nameof(SimulationButtonText));
    OnPropertyChanged(nameof(SimulationStatusText));
  }

  partial void OnLastUpdatedChanged(DateTime value)
  {
    OnPropertyChanged(nameof(LastUpdatedText));
  }

  private void Equipment_PropertyChanged(
      object? sender,
      System.ComponentModel.PropertyChangedEventArgs e)
  {
    if (e.PropertyName == nameof(Equipment.Status))
    {
      RefreshStatistics();
    }
  }

  private void EquipmentItems_CollectionChanged(
      object? sender,
      NotifyCollectionChangedEventArgs e)
  {
    if (e.OldItems is not null)
    {
      foreach (Equipment item in e.OldItems)
        item.PropertyChanged -= Equipment_PropertyChanged;
    }

    if (e.NewItems is not null)
    {
      foreach (Equipment item in e.NewItems)
        item.PropertyChanged += Equipment_PropertyChanged;
    }

    RefreshStatistics();
  }

  private void RefreshStatistics()
  {
    OnPropertyChanged(nameof(TotalEquipment));
    OnPropertyChanged(nameof(RunningEquipment));
    OnPropertyChanged(nameof(WarningEquipment));
    OnPropertyChanged(nameof(CriticalEquipment));
    OnPropertyChanged(nameof(StoppedEquipment));
  }

  [RelayCommand]
  private void StartEquipment()
  {
    if (SelectedEquipment is null)
      return;

    SelectedEquipment.Status = EquipmentStatus.Running;

    // Restore a safe demo temperature when restarting
    SelectedEquipment.Temperature = 65.0;
    SelectedEquipment.Pressure = 8.0;
  }

  [RelayCommand]
  private void StopEquipment()
  {
    if (SelectedEquipment is null)
      return;

    SelectedEquipment.Status = EquipmentStatus.Stopped;
    SelectedEquipment.Pressure = 0;
  }

  [RelayCommand]
  private void SetWarning()
  {
    if (SelectedEquipment is null)
      return;

    SelectedEquipment.Temperature = 85.0;
    SelectedEquipment.Status = EquipmentStatus.Warning;
  }

  [RelayCommand]
  private void SetCritical()
  {
    if (SelectedEquipment is null)
      return;

    SelectedEquipment.Temperature = 100.0;
    SelectedEquipment.Status = EquipmentStatus.Critical;
  }

  partial void OnIsDarkThemeChanged(bool value)
  {
    if (Application.Current is not null)
    {
      Application.Current.RequestedThemeVariant =
          value ? ThemeVariant.Dark : ThemeVariant.Light;
    }

    OnPropertyChanged(nameof(ThemeButtonText));
  }

  [RelayCommand]
  private void ToggleTheme()
  {
    IsDarkTheme = !IsDarkTheme;
  }

  public void Dispose()
  {
    _simulationTimer.Stop();
    _simulationTimer.Tick -= SimulationTimer_Tick;

    EquipmentItems.CollectionChanged -= EquipmentItems_CollectionChanged;

    foreach (var equipment in EquipmentItems)
    {
      equipment.PropertyChanged -= Equipment_PropertyChanged;
    }

    GC.SuppressFinalize(this);
  }
}
