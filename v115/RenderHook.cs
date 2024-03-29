﻿using Core.Abstracts;
using Hook;
using OpenGL;
using static Core.Utils.Interop;
using static korn;
using static OpenGL.Enums;

namespace v115;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook(Render render)
    {
        Render = render;

        SetHooks(
            new(GL.Interface->glEnable, ldftn(glEnable)),
            new(GL.Interface->glDisable, ldftn(glDisable)),
            new(GL.Interface->glOrtho, ldftn(glOrtho)),
            new(GL.Interface->glTranslatef, ldftn(glTrasnlateF)),
            new(GL.Interface->glScalef, ldftn(glScaleF)),

            SwapBuffersHook = new(GetProcAddress(GL.Interface->Module, "wglSwapBuffers"), ldftn(wglSwapBuffers))
        );
    }

    static Render Render;
    static HookFunction SwapBuffersHook;

    void glEnable(Cap cap)
    {
        if (Render.Enable(ref cap))
            GL.Enable(cap);
    }

    void glDisable(Cap cap)
    {
        if (Render.Disable(ref cap))
            GL.Disable(cap);
    }

    void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (Render.Ortho(left, right, bottom, top, zNear, zFar))
            GL.Ortho(left, right, bottom, top, zNear, zFar);
    }

    void glTrasnlateF(float x, float y, float z)
    {
        if (Render.TranslateF((x, y, z)))
            GL.Translatef(x, y, z);
    }

    void glScaleF(float x, float y, float z)
    {
        if (Render.ScaleF((x, y, z)))
            GL.Scalef(x, y, z);
    }

    void wglSwapBuffers(nint hdc)
    {
        ((delegate* unmanaged<nint, void>)SwapBuffersHook)(hdc);
        Render.SwapBuffers(hdc);
    }
}