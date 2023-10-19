#define CS

using Cetours;

namespace Core.Abstracts;
public unsafe abstract class AbstractRenderHook
{
    public AbstractRenderHook(AbstractRender render) => Render = render;

    public AbstractRender Render;

    protected HookFunction[] hooks;
    Dictionary<HookFunction, int> hookToOffset = new();

    protected void SetHooks(params HookFunction[] functions) => hooks = functions;

    public void Attach()
    {
        foreach (var hook in hooks)
        {
            _ = 3;
#if CS
            fixed (void** pe = &hook.Origin.Ptr)
            {
                Debug.WriteLine(hook.Origin.Addr.ToString("X"));
                //Detours.DetourAttach(pe, hook.Ripped.Ptr);
            }
#else
            hook.Attach();
#endif
            var offset = GetGLFuncOffset((nint)hook);
            if (offset != -1)
                hookToOffset[hook] = offset;
        }
    }

    public void PostAttach()
    {
        foreach (var hook in hooks)
            if (hookToOffset.ContainsKey(hook))
                ((nint*)GL.Interface)[hookToOffset[hook]] = (nint)hook;
    }

    int GetGLFuncOffset(nint addr)
    {
        var ptr = (nint*)GL.Interface;
        for (int i = 0; i < GLInterface.FunctionsCount; i++)
            if (ptr[i] == addr)
                return i;

        return -1;
    }
}