using Core;
using Hook;
using OpenGL;
using System.Runtime.InteropServices;
using static OpenGL.Enums;
using static Core.Utils.Interop;

namespace v112;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook(Render render) : base(render)
    {
        Render = render;

        hooks = new HookFunction[]
        {
            EnableHook = new HookFunction(GL.Interface.glEnable, (delegate* unmanaged<Cap, void>)&glEnable),
            DisableHook = new HookFunction(GL.Interface.glDisable, (delegate* unmanaged<Cap, void>)&glDisable),
            BeginHook = new HookFunction(GL.Interface.glBegin, (delegate* unmanaged<Mode, void>)&glBegin),
            OrthoHook = new HookFunction(GL.Interface.glOrtho, (delegate* unmanaged<double, double, double, double, double, double, void>)&glOrtho),
            TranslateFHook = new HookFunction(GL.Interface.glTranslatef, (delegate* unmanaged<float, float, float, void>)&glTrasnlateF),
            ScaleFHook = new HookFunction(GL.Interface.glScalef, (delegate* unmanaged<float, float, float, void>)&glScaleF),

            SwapBuffersHook = new HookFunction(GetProcAddress(GL.Interface.Base, "wglSwapBuffers"), (delegate* unmanaged<nint, void>)&wglSwapBuffers)
        };
    }

    public override void Attach()
    {
        foreach (var hook in hooks)
            hook.Attach();
    }

    public override void Detach()
    {
        foreach (var hook in hooks)
            hook.Detach();
    }

    public static Render Render;

    static HookFunction[] hooks;
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
    public static void SwapBuffers(nint hdc) => ((delegate* unmanaged<nint, void>)SwapBuffersHook)(hdc);

    [UnmanagedCallersOnly]
    static void glEnable(Cap cap)
    {
        if (Render.Enable(ref cap))
            Enable(cap);
    }

    [UnmanagedCallersOnly]
    static void glDisable(Cap cap)
    {
        if (Render.Disable(ref cap))
            Disable(cap);
    }

    [UnmanagedCallersOnly]
    static void glBegin(Mode mode)
    {
        if (Render.Begin(ref mode))
            Begin(mode);
    }

    [UnmanagedCallersOnly]
    static void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (Render.Ortho(ref left, ref right, ref bottom, ref top, ref zNear, ref zFar))
            Ortho(left, right, bottom, top, zNear, zFar);
    }

    [UnmanagedCallersOnly]  
    static void glTrasnlateF(float x, float y, float z)
    {
        if (Render.TranslateF(ref x, ref y, ref z))
            TranslateF(x, y, z);
    }

    [UnmanagedCallersOnly]
    static void glScaleF(float x, float y, float z)
    {
        if (Render.ScaleF(ref x, ref y, ref z))
            ScaleF(x, y, z);
    }

    [UnmanagedCallersOnly]
    static void wglSwapBuffers(nint hdc)
    {
        SwapBuffers(hdc);
        Render.SwapBuffers(hdc);
    }
}