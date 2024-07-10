namespace v115;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook(Render render)
    {
        Render = render;

        SetHooks(
            new(GL.Interface->glEnable, ldftn(glEnable)),
            new(GL.Interface->glDisable, ldftn(glDisable)),
            new(GL.Interface->glScalef, ldftn(glScaleF)),
            new(GL.Interface->glOrtho, ldftn(glOrtho)),
            DrawArraysHook = new(LwjglModule.glDrawArrays, ldftn(glDrawArrays)),
            VertexPointerHook = new(LwjglModule.glVertexPointer, ldftn(glVertexPointer)),
            SwapBuffersHook = new(GetProcAddress(GL.Interface->Module, "wglSwapBuffers"), ldftn(wglSwapBuffers))
        );
    }

    [AllowNull] static Render Render;
    [AllowNull] static HookFunction SwapBuffersHook;
    [AllowNull] static HookFunction DrawArraysHook;
    [AllowNull] static HookFunction VertexPointerHook;

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

    void glScaleF(float x, float y, float z)
    {
        Render.ScaleF((x, y, z));
        GL.Scalef(x, y, z);
    }

    void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        Render.Ortho(left, right, bottom, top, zNear, zFar);
        GL.Ortho(left, right, bottom, top, zNear, zFar);
    }

    void glDrawArrays(pointer env, pointer clazz, Mode mode, int first, int count)
    {
        calli(DrawArraysHook, env, clazz, mode, first, count);
        Render.DrawArrays(mode, first, count);
    }

    void glVertexPointer(pointer env, pointer clazz, int size, TexType type, int stride, pointer pointer)
    {
        Render.VertexPointer(size, type, stride, pointer);
        calli(VertexPointerHook, env, clazz, size, type, stride, pointer);
    }

    void wglSwapBuffers(nint hdc)
    {
        calli(SwapBuffersHook, hdc);
        Render.SwapBuffers(hdc);
    }
}