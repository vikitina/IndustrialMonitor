
using System;

namespace IndustrialMonitor.Models;

public record TelemetrySample(
    DateTime Timestamp,
    double Temperature,
    double Pressure
);
