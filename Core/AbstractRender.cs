namespace Core;
public abstract class AbstractRender
{
    public AbstractRender(Targets targets)
    {
        Targets = targets;
    }

    public Targets Targets;
}