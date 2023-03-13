using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keys = ESP.Interop.Keys;

namespace ESP;
public static class BindManager
{
    public static void StartReceive()
    {
        Interop.StartThread(() =>
        {
            while (true)
                foreach (Bind bind in Binds)
                    if ((Interop.GetAsyncKeyState(bind.Key) & 1) == 1)
                        bind.Func();
        });
    }

    public static List<Bind> Binds { get; private set; } = new List<Bind>();

    public static void Add(params Bind[] binds) => Binds.AddRange(binds);
}

public class Bind
{
    public Bind(Keys key, Action func)
    {
        Key = key;
        Func = func;
    }

    public Keys Key { get; init; }
    public Action Func { get; init; }
}