# ManiaVersionSelector
Utility that allows the selection of an installed TrackMania/ManiaPlanet instance when opening a `.Gbx` file

![MVS](D:\Data\Documents\GitHub\ManiaVersionSelector\MVS.jpg)

## Prerequisites
You need [.NET Core Desktop Runtime version 3.1 or above](https://dotnet.microsoft.com/download/dotnet-core/3.1) to run this application

## Installation
1. Download the latest release 

   [here]: https://github.com/stefan-baumann/ManiaVersionSelector/releases

   

2. Extract the contents of the zip file somewhere on your PC

3. Edit the `configuration.json` file to add/specify your game executable paths

4. Select `ManiaVersionSelector` as the default application for opening `.Gbx` files with using the Windows Explorer

## Todo list
- [ ] Icon, so that `.Gbx` files don't have the generic executable icon
- [ ] Configuration UI instead of manual `configuration.json` editing
- [ ] Support for commandline arguments for the launched applications
- [ ] Installer
- [ ] Automatic detection of appropriate game version?
