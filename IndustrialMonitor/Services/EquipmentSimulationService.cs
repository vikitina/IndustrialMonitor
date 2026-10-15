
using System;
using System.Collections.Generic;
using IndustrialMonitor.Models;

namespace IndustrialMonitor.Services;

public class EquipmentSimulationService
{
  private readonly Random _random = new();

  public void UpdateEquipment(IEnumerable<Equipment> equipmentItems)
  {
    var timestamp = DateTime.Now;

    foreach (var equipment in equipmentItems)
    {
      if (equipment.Status == EquipmentStatus.Stopped)
        continue;

      double temperatureChange =
          (_random.NextDouble() - 0.5) * 5.0;

      double pressureChange =
          (_random.NextDouble() - 0.5) * 0.8;

      equipment.Temperature = Math.Clamp(
          equipment.Temperature + temperatureChange,
          15.0,
          120.0
      );

      equipment.Pressure = Math.Clamp(
          equipment.Pressure + pressureChange,
          0.0,
          16.0
      );

      equipment.Status = equipment.Temperature switch
      {
        >= 95.0 => EquipmentStatus.Critical,
        >= 80.0 => EquipmentStatus.Warning,
        _ => EquipmentStatus.Running
      };

      equipment.AddTelemetrySample(timestamp);
    }
  }
}
