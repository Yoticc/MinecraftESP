namespace Core;
public abstract class AbstractRenderHook
{
    public AbstractRenderHook(AbstractRender render) => Render = render;

    public AbstractRender Render;

    public abstract void Attach();
    public abstract void Detach();
}