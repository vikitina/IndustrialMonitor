
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using IndustrialMonitor.Models;

namespace IndustrialMonitor.ViewModels;

public partial class EquipmentListViewModel : ViewModelBase, IDisposable
{
  private readonly ObservableCollection<Equipment> _source;

  public ObservableCollection<Equipment> FilteredEquipment { get; } = new();

  public IReadOnlyList<string> StatusOptions { get; } =
      new[] { "All", "Running", "Stopped", "Warning", "Critical" };

  public IReadOnlyList<string> SortOptions { get; } =
      new[] { "Name", "Temperature", "Pressure", "Status" };

  [ObservableProperty]
  private string _searchText = "";

  [ObservableProperty]
  private string _selectedStatus = "All";

  [ObservableProperty]
  private string _selectedSort = "Name";

  [ObservableProperty]
  private bool _sortDescending;

  [ObservableProperty]
  private Equipment? _selectedEquipment;

  public int VisibleCount => FilteredEquipment.Count;

  public EquipmentListViewModel(
      ObservableCollection<Equipment> equipmentItems)
  {
    _source = equipmentItems;

    _source.CollectionChanged += OnSourceCollectionChanged;

    foreach (var equipment in _source)
    {
      equipment.PropertyChanged += OnEquipmentPropertyChanged;
    }

    Refresh();
  }

  partial void OnSearchTextChanged(string value) => Refresh();

  partial void OnSelectedStatusChanged(string value) => Refresh();

  partial void OnSelectedSortChanged(string value) => Refresh();

  partial void OnSortDescendingChanged(bool value) => Refresh();

  private void OnSourceCollectionChanged(
      object? sender,
      System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
  {
    if (e.OldItems is not null)
    {
      foreach (Equipment equipment in e.OldItems)
        equipment.PropertyChanged -= OnEquipmentPropertyChanged;
    }

    if (e.NewItems is not null)
    {
      foreach (Equipment equipment in e.NewItems)
        equipment.PropertyChanged += OnEquipmentPropertyChanged;
    }

    Refresh();
  }

  private void OnEquipmentPropertyChanged(
      object? sender,
      PropertyChangedEventArgs e)
  {
    if (e.PropertyName is
        nameof(Equipment.Status) or
        nameof(Equipment.Temperature) or
        nameof(Equipment.Pressure))
    {
      Refresh();
    }
  }

  private void Refresh()
  {
    IEnumerable<Equipment> query = _source;

    if (!string.IsNullOrWhiteSpace(SearchText))
    {
      query = query.Where(e =>
          e.Name.Contains(
              SearchText.Trim(),
              StringComparison.OrdinalIgnoreCase) ||
          e.Id.Contains(
              SearchText.Trim(),
              StringComparison.OrdinalIgnoreCase));
    }

    if (SelectedStatus != "All")
    {
      query = query.Where(e =>
          e.Status.ToString() == SelectedStatus);
    }

    query = SelectedSort switch
    {
      "Temperature" => SortDescending
          ? query.OrderByDescending(e => e.Temperature)
          : query.OrderBy(e => e.Temperature),

      "Pressure" => SortDescending
          ? query.OrderByDescending(e => e.Pressure)
          : query.OrderBy(e => e.Pressure),

      "Status" => SortDescending
          ? query.OrderByDescending(e => e.Status)
          : query.OrderBy(e => e.Status),

      _ => SortDescending
          ? query.OrderByDescending(e => e.Name)
          : query.OrderBy(e => e.Name)
    };

    var result = query.ToList();

    FilteredEquipment.Clear();

    foreach (var equipment in result)
      FilteredEquipment.Add(equipment);

    OnPropertyChanged(nameof(VisibleCount));
  }

  public void Dispose()
  {
    _source.CollectionChanged -= OnSourceCollectionChanged;

    foreach (var equipment in _source)
      equipment.PropertyChanged -= OnEquipmentPropertyChanged;
  }
}
