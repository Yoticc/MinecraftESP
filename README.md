# MinecraftESP

Internal Minecraft ESP hack written in C# using Korn \
Avaible Version: 1.0.0 - 1.14.4 and additional projects: Cristalix

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

All available binds are in the config file, which is created upon first launch. But by default these are Numpad buttons

Use
------------------------------
**[!] Keep `Wrapper.dll` and `Minecraft.dll` in the same folder** \
Can be used any injector. The most popular — `Process Hacker 2`, `Extreme Injector` (good for getting error info) and `GH Injector` \
For injection — inject `Wrapper.dll` \
If you need use another from LoadLibraryW injection method, inject first `MinecraftESP` and after it `Wrapper.dll` by your injection method

Screenshot
------------------------------
![image](https://user-images.githubusercontent.com/55879406/227733763-a168a6be-3b8e-4b5d-9d3d-5c50dd734b1c.png)

Used Libraries
------------------------------
**Cetours** [![NuGet](https://img.shields.io/nuget/v/Cetours.svg)](https://www.nuget.org/packages/Cetours) \
**Yotic.Hook** [![NuGet](https://img.shields.io/nuget/v/Yotic.Hook.svg)](https://www.nuget.org/packages/Yotic.Hook) \
**Yotic.OpenGL** [![NuGet](https://img.shields.io/nuget/v/Yotic.OpenGL.svg)](https://www.nuget.org/packages/Yotic.OpenGL) \
**Yotic.Memory.Extensions** [![NuGet](https://img.shields.io/nuget/v/Yotic.Memory.Extensions.svg)](https://www.nuget.org/packages/Yotic.Memory.Extensions)

Build
------------------------------
Build solution and enter the command `dotnet publish -r win-x64 -c Release` in Powershell for developers
![image](https://media.discordapp.net/attachments/940166965216051232/1168738178694381568/image.png) \
It will create `MinecraftESP.dll` in `MinecraftESP\MinecraftESP\bin\Release\net7.0\win-x64\native`\
`Wrapper.dll` you can get from `MinecraftESP\x64\Release`
