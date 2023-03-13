using Hook;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Render = ESP.Render;

namespace ESP;
public unsafe class RenderHook
{
    public static void Init(Render render)
    {
        Render = render;

        EnableHook = new HookFunction(GL.Interface.glEnable, (delegate* unmanaged<Cap, void>)&glEnable).Attach();
        DisableHook = new HookFunction(GL.Interface.glDisable, (delegate* unmanaged<Cap, void>)&glDisable).Attach();
        BeginHook = new HookFunction(GL.Interface.glBegin, (delegate* unmanaged<Mode, void>)&glBegin).Attach();
    }
    public static Render Render { get; private set; }

    public static HookFunction EnableHook;
    public static HookFunction DisableHook;
    public static HookFunction BeginHook;

    public static void Enable(Cap cap) => ((delegate* unmanaged<Cap, void>)EnableHook)(cap);
    public static void Disable(Cap cap) => ((delegate* unmanaged<Cap, void>)DisableHook)(cap);
    public static void Begin(Mode mode) => ((delegate* unmanaged<Mode, void>)BeginHook)(mode);

    [UnmanagedCallersOnly]
    private static void glEnable(Cap cap)
    {
        if (Render.Enable(ref cap))
            Enable(cap);
    }

    [UnmanagedCallersOnly]
    private static void glDisable(Cap cap)
    {
        if (Render.Disable(ref cap))
            Disable(cap);
    }

    [UnmanagedCallersOnly]
    private static void glBegin(Mode mode)
    {
        if (Render.Begin(ref mode))
            Begin(mode);
    }
}