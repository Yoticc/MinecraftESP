namespace Core;
public unsafe static class BindManager
{
    static BindManager()
    {
        threadwhile(() =>
        {
            foreach (Bind bind in Binds)
                if ((user32.GetAsyncKeyState((int)bind.Key) & 1) == 1)
                    if (IsCursorHide() && IsWindowActive())
                        bind.Func();
            Thread.Sleep(5);            
        });
    }

    public static List<Bind> Binds = [];

    public static void Add(IEnumerable<Bind> binds) => Binds.AddRange(binds);
}

public unsafe record Bind(Keys Key, Action Func)
{
    public Bind(Keys key, bool* ptr) : this(key, () => *ptr = !*ptr) { }
}