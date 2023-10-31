using Native = System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
using static Core.Utils.Interop;
using static OpenGL.Enums;
using Core.Abstracts;
using OpenGL;
using Hook;

namespace vCristalix;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook(Render render)
    {
        Render = render;

        SetHooks(
            new(GL.Interface->glEnable, (delegate* unmanaged<Cap, void>)&glEnable),
            new(GL.Interface->glDisable, (delegate* unmanaged<Cap, void>)&glDisable),
            new(GL.Interface->glOrtho, (delegate* unmanaged<double, double, double, double, double, double, void>)&glOrtho),
            new(GL.Interface->glTranslatef, (delegate* unmanaged<float, float, float, void>)&glTrasnlateF),
            new(GL.Interface->glScaled, (delegate* unmanaged<double, double, double, void>)&glScaleD),
            new(GL.Interface->glScalef, (delegate* unmanaged<float, float, float, void>)&glScaleF),

            SwapBuffersHook = new(GetProcAddress(GL.Interface->Module, "wglSwapBuffers"), (delegate* unmanaged<nint, void>)&wglSwapBuffers)
        );
    }

    static Render Render;
    static HookFunction SwapBuffersHook;

    [Native]
    static void glEnable(Cap cap)
    {
        if (Render.Enable(ref cap))
            GL.Enable(cap);
    }

    [Native]
    static void glDisable(Cap cap)
    {
        if (Render.Disable(ref cap))
            GL.Disable(cap);
    }

    [Native]
    static void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (Render.Ortho(left, right, bottom, top, zNear, zFar))
            GL.Ortho(left, right, bottom, top, zNear, zFar);
    }

    [Native]
    static void glTrasnlateF(float x, float y, float z)
    {
        if (Render.TranslateF((x, y, z)))
            GL.Translatef(x, y, z);
    }

    [Native]
    static void glScaleD(double x, double y, double z)
    {
        if (Render.ScaleD((x, y, z)))
            GL.Scaled(x, y, z);
    }

    [Native]
    static void glScaleF(float x, float y, float z)
    {
        if (Render.ScaleF((x, y, z)))
            GL.Scalef(x, y, z);
    }

    [Native]
    static void wglSwapBuffers(nint hdc)
    {
        ((delegate* unmanaged<nint, void>)SwapBuffersHook)(hdc);
        Render.SwapBuffers(hdc);
    }
}