﻿namespace Core.Utils;
public unsafe static class BindManager
{
    static BindManager()
    {
        StartThread(() =>
        {
            while (true)
            {
                foreach (Bind bind in Binds)
                    if ((GetAsyncKeyState(bind.Key) & 1) == 1)
                        if (IsCursorHide() && IsWindowActive())
                            bind.Func();
                Thread.Sleep(5);
            }
        });
    }

    public static List<Bind> Binds { get; set; } = new();

    public static void Add(IEnumerable<Bind> binds) => Binds.AddRange(binds);
}

public unsafe record Bind(Keys Key, Action Func)
{
    public Bind(Keys key, bool* ptr) : this(key, () => *ptr = !*ptr) { }
}