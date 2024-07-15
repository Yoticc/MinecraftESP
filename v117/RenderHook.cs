using Buffer = OpenGL.Enums.Buffer;

namespace v117;
public unsafe class RenderHook : AbstractRenderHook
{
    public RenderHook()
    {
        SetHooks(
            new(GL.Interface->glEnable, ldftn(glEnable)),
            new(GL.Interface->glDisable, ldftn(glDisable)),
            DrawElementsHook = new(LwjglModule.glDrawElements, ldftn(glDrawElements)),
            BufferDataHook = new(LwjglModule.glBufferData, ldftn(glBufferData))
        );
    }

    static Render Render = new();
    [AllowNull] static HookFunction DrawElementsHook, BufferDataHook;

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

    void glDrawElements(pointer env, pointer @class, Mode mode, int count, BUType type, pointer indicies)
    {
        Render.DrawElements(ref mode, count, type, indicies);
        calli(DrawElementsHook, env, @class, mode, count, type, indicies);
    }

    void glBufferData(pointer env, pointer @class, Buffer type, int size, pointer data, BufferUsage usage)
    {
        Render.BufferData(type, size, data, usage);
        calli(BufferDataHook, env, @class, type, size, data, usage);
    }
}