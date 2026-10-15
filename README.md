# IndustrialMonitor

A work-in-progress industrial equipment monitoring dashboard built with **C#, .NET 10, Avalonia UI, and MVVM**.

## Features

- Equipment monitoring dashboard with KPI cards
- Live simulation of temperature and pressure readings
- Equipment status monitoring and control commands
- Temperature history visualization
- Reactive UI updates using CommunityToolkit.Mvvm
- Light and dark themes
- Cross-platform architecture with Desktop and Browser projects

## Tech Stack

- C# / .NET 10
- Avalonia UI / XAML
- CommunityToolkit.Mvvm
- ObservableCollection and data binding

## Run the Desktop Application

Requires the .NET 10 SDK.

```bash
dotnet restore
dotnet run --project IndustrialMonitor.Desktop
```

## Project Status

This is a portfolio demonstration of an industrial monitoring interface. Equipment data is simulated; no physical equipment or production backend is connected.

Currently developing: equipment management view, navigation, and browser deployment.