# MinecraftESP

Internal Minecraft ESP hack written in C# using Korn \
Available version: 1.0.0 - 1.16.5 and additional projects: Cristalix

Functions
------------------------------
- **PlayerESP** - Highlights players
- **ChestESP** - Highlights all types of chests (doesn't support dispensers, hoppers, etc.)
- **SignESP** - Highlights signs
- **ItemESP** - Highlights items
- **NoFog** - Remove fog
- **NoLight** - Remove models light
- **NoBackground** - Remove GUI background
- **CaveViewer** - Breaks game render, highlighting caves

All available binds are in the config file that is created at first inject. But by default they are Numpad buttons

Use
------------------------------
Inject `Minecraft.dll` \
Any injector can be used. The most popular — `Process Hacker 2`, `Extreme Injector` (good for getting error info) and `GH Injector`

Screenshot
------------------------------
![image](https://github.com/user-attachments/assets/9d6cd45d-59cb-456e-a237-4d560e437a59)

Used Libraries
------------------------------
DotnetNative project with Korn analyzer \
[Yotic.Linq](https://github.com/Yoticc/Yotic.Math) \
[Yotic.Math](https://github.com/Yoticc/Yotic.Linq) \
**Cetours** [![NuGet](https://img.shields.io/nuget/v/Cetours.svg)](https://www.nuget.org/packages/Cetours) \
**Yotic.Hook** [![NuGet](https://img.shields.io/nuget/v/Yotic.Hook.svg)](https://www.nuget.org/packages/Yotic.Hook) \
**Yotic.OpenGL** [![NuGet](https://img.shields.io/nuget/v/Yotic.OpenGL.svg)](https://www.nuget.org/packages/Yotic.OpenGL) \
**Yotic.Memory.Extensions** [![NuGet](https://img.shields.io/nuget/v/Yotic.Memory.Extensions.svg)](https://www.nuget.org/packages/Yotic.Memory.Extensions) \
**Memory.Manipulation** [![NuGet](https://img.shields.io/nuget/v/Memory.Manipulation.svg)](https://www.nuget.org/packages/Memory.Manipulation)

Build
------------------------------
YOU CANNOT BUILD IT RIGHT NOW. USE THE PREVIOUS VERSION. YOU CAN DOWNLOAD CODE FROM 1.0.1 RELEASE.

Special Thanks
------------------------------
**August Vishnevsky** - for his original idea and C++ [source code](https://github.com/aurenex/simple-esp)
