using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Keys = ESP.Utils.Interop.Keys;

namespace ESP.Utils;
public unsafe static class BindManager
{
    #region Interop
    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct CURSORINFO
    {
        public int cbSize;
        public int flags;
        public IntPtr hCursor;
        public POINT ptScreenPos;
    }

    [DllImport("user32")]
    private static extern bool GetCursorInfo(ref CURSORINFO pci);

    private static bool IsCursorHide()
    {
        CURSORINFO cur = new CURSORINFO();
        cur.cbSize = sizeof(CURSORINFO);
        GetCursorInfo(ref cur);

        int realFlag = cur.hCursor.ToInt32();
        return realFlag > 66000 || realFlag < 65000;
    }

    [DllImport("user32")]
    private static extern IntPtr GetForegroundWindow();

    private static bool IsWindowActive()
    {
        IntPtr activeWindow = GetForegroundWindow();
        IntPtr procWindow = Process.GetCurrentProcess().MainWindowHandle;
        return activeWindow == procWindow;
    }
    #endregion

    static BindManager()
    {
        Interop.StartThread(() =>
        {
            while (true)
                foreach (Bind bind in Binds)
                    if ((Interop.GetAsyncKeyState(bind.Key) & 1) == 1)
                        if (IsCursorHide() && IsWindowActive())
                            bind.Func();
        });
    }

    public static List<Bind> Binds { get; private set; } = new List<Bind>();

    public static void Add(IList<Bind> binds) => Binds.AddRange(binds);
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