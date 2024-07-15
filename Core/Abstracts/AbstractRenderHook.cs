namespace Core;
public unsafe abstract class AbstractRenderHook
{
    protected HookFunction[] hooks = [];

    protected void SetHooks(params HookFunction[] functions) => hooks = functions;

    public AbstractRenderHook Attach()
    {
        var glInterface = (nint*)GL.Interface;
        foreach (var hook in hooks)
        {
            hook.Attach();
            for (var i = 0; i < GLInterface.FunctionsCount; i++)
                if (glInterface[i] == hook.Origin.Pointer)
                {
                    glInterface[i] = (nint)hook;
                    break;
                }
        }

        return this;
    }
}