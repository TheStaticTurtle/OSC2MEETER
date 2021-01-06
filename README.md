# OSC2MEETER
Convert OSC messages back and forth to control the voicemeeter application

This was intended to be used with the combination of TouchOSC (for which you can download an example layout in the relase page) but any OSC control app will do just fine
The `VoicemeeterRemote.cs` file can be copied to any project and is a wrapper arround the VoicemeeterRemote dll, you can track the development progress down below

| OSC2MEETER TodoList               | Status |
|-----------------------------------|-----|
| Send / Recv Fader postion         | ✅ |
| Send / Recv Mute                  | ✅ |
| Recv strip name for inputs        | ✅ |
| Command line options              | ❌ (Hardcoded send IP address) |

| Voicemeeter Remote API Wrapper TodoList      | Status |
|----------------------------------------------|-----|
| Parametters                                  | ✅ |
| Levels                                       | ✅ |
| AudioCallback                                | ❌ |
| MacroButtons                                 | ❌ |
