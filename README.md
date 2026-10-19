# IndustrialMonitor

[Open IndustrialMonitor in your browser](https://industrial-monitor-eta.vercel.app/)

A cross-platform industrial equipment monitoring dashboard built with **C#, .NET 10, Avalonia UI, and MVVM**.

The project demonstrates the development of responsive industrial user interfaces, real-time data visualization, equipment management, and reactive state updates using Avalonia UI.

The same application interface can run as a **desktop application** and in a **web browser via WebAssembly**.

## Features

### Dashboard
- KPI cards displaying equipment statistics
- Live simulation of temperature and pressure readings
- Equipment status monitoring
- Temperature history visualization
- Equipment details and control commands
- Reactive UI updates

### Equipment Management
- Equipment list with search, filtering, and sorting
- Equipment details and status information
- Simulated equipment control operations

### User Interface
- Responsive layouts for different screen sizes
- Sidebar navigation
- Light and dark themes
- Reusable Avalonia UI components
- Data binding and MVVM architecture

### Cross-Platform Support
- Desktop application
- Browser application using WebAssembly
- Shared UI and application logic

## Tech Stack

- **Language:** C#
- **Framework:** .NET 10
- **UI:** Avalonia UI 12 / XAML
- **Architecture:** MVVM
- **MVVM Toolkit:** CommunityToolkit.Mvvm
- **State and Data Binding:** ObservableCollection, observable properties, commands
- **Browser Runtime:** WebAssembly
- **Graphics:** SkiaSharp

## Project Structure

```text
IndustrialMonitor/
├── IndustrialMonitor/          # Shared UI, ViewModels and application logic
├── IndustrialMonitor.Desktop/  # Desktop application entry point
└── IndustrialMonitor.Browser/  # WebAssembly application entry point
```

## Run the Desktop Application

Requires the .NET 10 SDK.

```bash
dotnet restore
dotnet run --project IndustrialMonitor.Desktop
```

## Run the Browser Application

Requires the .NET 10 SDK and WebAssembly build tools.

Install the workload:

```bash
dotnet workload install wasm-tools
```

Start the browser application:

```bash
dotnet run --project IndustrialMonitor.Browser
```

Open the local URL displayed in the terminal.

## Build for Browser Deployment

```bash
dotnet publish IndustrialMonitor.Browser -c Release
```

The generated static application files are located in:

```text
IndustrialMonitor.Browser/bin/Release/net10.0-browser/publish/wwwroot
```

The browser application can be deployed to a static hosting service such as GitHub Pages.

## Architecture

The application follows the **Model-View-ViewModel (MVVM)** pattern.

- **Models** represent equipment and telemetry data.
- **ViewModels** manage application state, commands, and UI updates.
- **Views** define the interface using Avalonia XAML.
- **CommunityToolkit.Mvvm** provides observable properties and command infrastructure.

The project separates shared application logic from platform-specific entry points.

## Project Status

**Portfolio demonstration — actively developed.**

IndustrialMonitor is designed to demonstrate practical Avalonia UI development skills, including responsive XAML layouts, MVVM architecture, interactive dashboards, and cross-platform deployment.

Equipment data and telemetry are simulated. No physical industrial equipment or production backend is connected.

## Planned Improvements

- Additional equipment management features
- Enhanced telemetry visualization
- UI and architecture refinements
- Automated testing

## License

No license has been specified for this project.