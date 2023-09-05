namespace ESP.Utils;
public unsafe static class BindManager
{
    static BindManager()
    {
        StartThread(() =>
        {
            while (true)
                foreach (Bind bind in Binds)
                    if ((GetAsyncKeyState(bind.Key) & 1) == 1)
                        if (IsCursorHide() && IsWindowActive())
                            bind.Func();
        });
    }

    public static List<Bind> Binds { get; set; } = new List<Bind>();

    public static void Add(IList<Bind> binds) => Binds.AddRange(binds);
}

public record Bind(Keys Key, Action Func);