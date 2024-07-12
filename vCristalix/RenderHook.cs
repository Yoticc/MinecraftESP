﻿namespace vCristalix;
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
            new(GL.Interface->glScaled, ldftn(glScaleD)),
            new(GL.Interface->glScalef, ldftn(glScaleF)),
            SwapBuffersHook = new(OpenGLModule.wglSwapBuffers, ldftn(wglSwapBuffers))
        );
    }

    [AllowNull] static Render Render;
    [AllowNull] static HookFunction SwapBuffersHook;

    void glEnable(Cap cap)
    {
        if (Render.Enable(cap))
            GL.Enable(cap);
    }

    void glDisable(Cap cap)
    {
        if (Render.Disable(cap))
            GL.Disable(cap);
    }

    void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        Render.Ortho(left, right, bottom, top, zNear, zFar);
        GL.Ortho(left, right, bottom, top, zNear, zFar);
    }

    void glTrasnlateF(float x, float y, float z)
    {
        Render.TranslateF((x, y, z));
        GL.Translatef(x, y, z);
    }

    void glScaleD(double x, double y, double z)
    {
        Render.ScaleD((x, y, z));
        GL.Scaled(x, y, z);
    }

    void glScaleF(float x, float y, float z)
    {
        Render.ScaleF((x, y, z));
        GL.Scalef(x, y, z);
    }

    void wglSwapBuffers(nint hdc)
    {
        calli(SwapBuffersHook, hdc);
        Render.SwapBuffers(hdc);
    }
}