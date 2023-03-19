using ESP.Utils;
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
        OrthoHook = new HookFunction(GL.Interface.glOrtho, (delegate* unmanaged<double, double, double, double, double, double, void>)&glOrtho).Attach();
        TranslateFHook = new HookFunction(GL.Interface.glTranslatef, (delegate* unmanaged<float, float, float, void>)&glTrasnlateF).Attach();
        ScaleFHook = new HookFunction(GL.Interface.glScalef, (delegate* unmanaged<float, float, float, void>)&glScaleF).Attach();
        
        SwapBuffersHook = new HookFunction(Interop.GetProcAddress(GL.Interface.Base, "wglSwapBuffers").ToPointer(), (delegate* unmanaged<IntPtr, void>)&wglSwapBuffers).Attach();
    }
    public static Render Render { get; private set; }

    public static HookFunction EnableHook;
    public static HookFunction DisableHook;
    public static HookFunction BeginHook;
    public static HookFunction OrthoHook;
    public static HookFunction TranslateFHook;
    public static HookFunction ScaleFHook;
    public static HookFunction SwapBuffersHook;

    public static void Enable(Cap cap) => ((delegate* unmanaged<Cap, void>)EnableHook)(cap);
    public static void Disable(Cap cap) => ((delegate* unmanaged<Cap, void>)DisableHook)(cap);
    public static void Begin(Mode mode) => ((delegate* unmanaged<Mode, void>)BeginHook)(mode);
    public static void Ortho(double left, double right, double bottom, double top, double zNear, double zFar) => ((delegate* unmanaged<double, double, double, double, double, double, void>)OrthoHook)(left, right, bottom, top, zNear, zFar);
    public static void TranslateF(float x, float y, float z) => ((delegate* unmanaged<float, float, float, void>)TranslateFHook)(x, y, z);
    public static void ScaleF(float x, float y, float z) => ((delegate* unmanaged<float, float, float, void>)ScaleFHook)(x, y, z);
    public static void SwapBuffers(IntPtr hdc) => ((delegate* unmanaged<IntPtr, void>)SwapBuffersHook)(hdc);

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

    [UnmanagedCallersOnly]
    private static void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (Render.Ortho(ref left, ref right, ref bottom, ref top, ref zNear, ref zFar))
            Ortho(left, right, bottom, top, zNear, zFar);
    }

    [UnmanagedCallersOnly]
    private static void glTrasnlateF(float x, float y, float z)
    {
        if (Render.TranslateF(ref x, ref y, ref z))
            TranslateF(x, y, z);
    }

    [UnmanagedCallersOnly]
    private static void glScaleF(float x, float y, float z)
    {
        if (Render.ScaleF(ref x, ref y, ref z))
            ScaleF(x, y, z);
    }

    [UnmanagedCallersOnly]
    private static void wglSwapBuffers(IntPtr hdc)
    {
        SwapBuffers(hdc);
        Render.SwapBuffers(hdc);
    }
}