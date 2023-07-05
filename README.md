# Screenpresso License Generator
Allows you to generate and activate a copy of [Screenpresso v2.1.12](https://screenpresso.com/) using offline manual activation.

# About
Developer holds no responsibility with what people decide to do with this app. It was developed strictly for demonstration purposes only.
Developed under the following conditions:
- Visual Studio 2022 (17.6.4)
- v4.8.0 .NET Framework

# Usage
Download the project repo and open with Visual Studio. 
Executable in `/bin/` folder.
If building your own version, you must ensure the following three files are in the same directory with each other:
- /bin/`ScreenpressoKG.exe`
- /bin/`ScreenpressoKG.exe.config`
- /bin/`readme.md`

# App.config
This file holds default values that the app uses when launching. You shouldn't need to modify these, but they're provided just in case:
```xml
  <appSettings>
    <add key="username_default" value="Aetherinox"/>
    <add key="editions_list" value="ActivationKey,LicenseBoundToSoftwareName,LicenseBoundToHardDrive,LicenseCorporate,LicenseBoundToHardDrive2"/>
    <add key="ClientSettingsProvider.ServiceUri" value=""/>
  </appSettings>
```
Don't modify these unless you know what you're doing, improperly configured, the Activation and Response will not generate into a valid serial key.

# Previews
