# ManiaVersionSelector
Utility that allows the selection of an installed TrackMania/ManiaPlanet instance when opening a `.Gbx` file

## Prerequisites
You need [.NET Core Desktop Runtime version 3.1 or above](https://dotnet.microsoft.com/download/dotnet-core/3.1) to run this application

## Installation
1. Extract the contents of the zip file somewhere on your PC
2. Edit the `configuration.json` file to add/specify your game executable paths
3. Select `ManiaVersionSelector` as the default application for opening `.Gbx` files with using the Windows Explorer

## Todo list
- [ ] Icon, so that `.Gbx` files don't have the generic executable icon
- [ ] Configuration UI instead of manual `configuration.json` editing
- [ ] Support for commandline arguments for the launched applications
- [ ] Installer
- [ ] Automatic detection of appropriate game version?
