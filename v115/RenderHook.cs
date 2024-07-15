namespace v115;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook()
    {
        SetHooks(
            new(GL.Interface->glEnable, ldftn(glEnable)),
            new(GL.Interface->glDisable, ldftn(glDisable)),
            new(GL.Interface->glScalef, ldftn(glScaleF)),
            DrawArraysHook = new(LwjglModule.glDrawArrays, ldftn(glDrawArrays)),
            VertexPointerHook = new(LwjglModule.glVertexPointer, ldftn(glVertexPointer))
        );
    }

    static Render Render = new();
    [AllowNull] static HookFunction DrawArraysHook, VertexPointerHook;

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

    void glScaleF(float x, float y, float z)
    {
        Render.ScaleF((x, y, z));
        GL.Scalef(x, y, z);
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
}