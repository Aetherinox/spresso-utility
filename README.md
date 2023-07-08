# Screenpresso License Generator
Allows you to generate and activate a copy of [Screenpresso v2.1.12](https://screenpresso.com/) using offline manual activation.

# About
Developer holds no responsibility with what people decide to do with this app. It was developed strictly for demonstration purposes only.
Developed under the following conditions:
- Visual Studio 2022 (17.6.4)
- v4.8.0 .NET Framework
- C# language

# Usage
If you wish to simply use the keygen, head over to the [Releases](https://github.com/Aetherinox/ScreenpressoKeygen/releases) section and download the latest binary as a `zip` or `rar`. The binary release should only contain two files:
- /bin/`ScreenpressoKG.exe`
- /bin/`ScreenpressoKG.exe.config`

Make sure these two files are both in the same folder.

# Build
Download the source files and launch the `.csproj` project file in Visual Studio.

If you decide to modify or re-build my code, you are to not re-distribute. Unlike a lot of keygens, my files are free of malware, and I do not want people taking advantage of a quick solution that you can dump your non-sense malware into and re-distribute.

If you're looking to do a quick credits swap and re-distribute just so you can make a name for yourself; I'd highly recommend you actually learn C# and make something yourself.

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
![Main Screen](https://i.imgur.com/sNCXVle.png)
![Block Host Confirmation](https://i.imgur.com/ucWP4Hk.png)
![Generate License Key](https://i.imgur.com/gukUKPU.png)
![Interface + Help Menu](https://i.imgur.com/j2erqaA.png)
![Valid License](https://i.imgur.com/uVSX2CA.png)
![Activation Complete](https://i.imgur.com/vHIuhf8.png)
![Verifying License](https://i.imgur.com/QWIMaNd.png)