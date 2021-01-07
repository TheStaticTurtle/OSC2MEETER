# OSC2MEETER
Convert OSC messages back and forth to control the voicemeeter application

This was intended to be used with the combination of TouchOSC (for which you can download an example layout in the relase page) but any OSC control app will do just fine

The `VoicemeeterRemote.cs` file can be copied to any project and used as wrapper arround the VoicemeeterRemote dll, it should automatically detect it's emplacement and use the 32bit/64bit version accordingly, you can track the development progress down below

| OSC2MEETER TodoList               | Status |
|-----------------------------------|-----|
| Send / Recv Fader postion         | ✅ |
| Send / Recv Mute                  | ✅ |
| Recv strip name for inputs        | ✅ |
| Command line options              | ✅ |

| Voicemeeter Remote API Wrapper TodoList      | Status |
|----------------------------------------------|-----|
| Login/Logout/Version/Run                     | ✅ |
| Parametters                                  | ✅ |
| Levels                                       | ✅ |
| AudioCallback                                | ❌ |
| MacroButtons                                 | ❌ |

## Dependencies
 - https://github.com/ValdemarOrn/SharpOSC (download dll)
 - https://github.com/commandlineparser/commandline (nuget)
 
## Sreenshots of the TouchOSC layout
<img src="https://data.thestaticturtle.fr/ShareX/2021/01/07/Screenshot_20210107-002952.jpg" width="200" /> <img src="https://data.thestaticturtle.fr/ShareX/2021/01/07/Screenshot_20210107-002857.jpg" width="200" /> <img src="https://data.thestaticturtle.fr/ShareX/2021/01/07/Screenshot_20210107-002958.jpg" width="200" />
