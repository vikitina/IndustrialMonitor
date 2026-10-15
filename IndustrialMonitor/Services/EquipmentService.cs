
using System.Collections.Generic;
using IndustrialMonitor.Models;

namespace IndustrialMonitor.Services;

public class EquipmentService
{
  public List<Equipment> GetEquipment()
  {
    return new List<Equipment>
        {
            new()
            {
                Id = "A-01",
                Name = "Hydraulic Pump A-01",
                Status = EquipmentStatus.Running,
                Temperature = 64.5,
                Pressure = 8.2
            },
            new()
            {
                Id = "B-04",
                Name = "Compressor B-04",
                Status = EquipmentStatus.Warning,
                Temperature = 88.1,
                Pressure = 11.4
            },
            new()
            {
                Id = "C-02",
                Name = "Cooling Unit C-02",
                Status = EquipmentStatus.Critical,
                Temperature = 102.3,
                Pressure = 4.8
            },
            new()
            {
                Id = "D-07",
                Name = "Conveyor D-07",
                Status = EquipmentStatus.Running,
                Temperature = 45.2,
                Pressure = 6.1
            },
            new()
            {
                Id = "E-03",
                Name = "Ventilation Unit E-03",
                Status = EquipmentStatus.Stopped,
                Temperature = 24.0,
                Pressure = 0.0
            },
            new()
            {
                Id = "F-06",
                Name = "Hydraulic Pump F-06",
                Status = EquipmentStatus.Running,
                Temperature = 59.7,
                Pressure = 7.9
            }
        };
  }
}
