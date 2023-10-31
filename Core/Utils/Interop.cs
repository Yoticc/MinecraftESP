using Microsoft.Win32.SafeHandles;

namespace Core.Utils;

#region Struct
[StructLayout(LayoutKind.Sequential)]
public record struct POINT(int X, int Y);
[StructLayout(LayoutKind.Sequential)]
public record struct CURSORINFO(int Size, int Flags, nint Cursor, Point ScreenPos);
#endregion
#region Enum
[Flags]
public enum LoadLibraryFlags : uint
{
    None = 0,
    DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
    LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
    LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
    LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
    LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
    LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
    LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
    LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
    LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
    LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
    LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
    LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 0x00000080,
    LOAD_LIBRARY_SAFE_CURRENT_DIRS = 0x00002000,
}
public enum Keys
{
    KeyCode = 0x0000FFFF,
    Modifiers = unchecked((int)0xFFFF0000),
    None = 0x00,
    LButton = 0x01,
    RButton = 0x02,
    Cancel = 0x03,
    MButton = 0x04,
    XButton1 = 0x05,
    XButton2 = 0x06,
    Back = 0x08,
    Tab = 0x09,
    LineFeed = 0x0A,
    Clear = 0x0C,
    Return = 0x0D,
    Enter = Return,
    ShiftKey = 0x10,
    ControlKey = 0x11,
    Menu = 0x12,
    Pause = 0x13,
    Capital = 0x14,
    CapsLock = 0x14,
    KanaMode = 0x15,
    HanguelMode = 0x15,
    HangulMode = 0x15,
    JunjaMode = 0x17,
    FinalMode = 0x18,
    HanjaMode = 0x19,
    KanjiMode = 0x19,
    Escape = 0x1B,
    IMEConvert = 0x1C,
    IMENonconvert = 0x1D,
    IMEAccept = 0x1E,
    IMEAceept = IMEAccept,
    IMEModeChange = 0x1F,
    Space = 0x20,
    Prior = 0x21,
    PageUp = Prior,
    Next = 0x22,
    PageDown = Next,
    End = 0x23,
    Home = 0x24,
    Left = 0x25,
    Up = 0x26,
    Right = 0x27,
    Down = 0x28,
    Select = 0x29,
    Print = 0x2A,
    Execute = 0x2B,
    Snapshot = 0x2C,
    PrintScreen = Snapshot,
    Insert = 0x2D,
    Delete = 0x2E,
    Help = 0x2F,
    D0 = 0x30,
    D1 = 0x31,
    D2 = 0x32,
    D3 = 0x33,
    D4 = 0x34,
    D5 = 0x35,
    D6 = 0x36,
    D7 = 0x37,
    D8 = 0x38,
    D9 = 0x39,
    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A,
    LWin = 0x5B,
    RWin = 0x5C,
    Apps = 0x5D,
    Sleep = 0x5F,
    NumPad0 = 0x60,
    NumPad1 = 0x61,
    NumPad2 = 0x62,
    NumPad3 = 0x63,
    NumPad4 = 0x64,
    NumPad5 = 0x65,
    NumPad6 = 0x66,
    NumPad7 = 0x67,
    NumPad8 = 0x68,
    NumPad9 = 0x69,
    Multiply = 0x6A,
    Add = 0x6B,
    Separator = 0x6C,
    Subtract = 0x6D,
    Decimal = 0x6E,
    Divide = 0x6F,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,
    NumLock = 0x90,
    Scroll = 0x91,
    LShiftKey = 0xA0,
    RShiftKey = 0xA1,
    LControlKey = 0xA2,
    RControlKey = 0xA3,
    LMenu = 0xA4,
    RMenu = 0xA5,
    BrowserBack = 0xA6,
    BrowserForward = 0xA7,
    BrowserRefresh = 0xA8,
    BrowserStop = 0xA9,
    BrowserSearch = 0xAA,
    BrowserFavorites = 0xAB,
    BrowserHome = 0xAC,
    VolumeMute = 0xAD,
    VolumeDown = 0xAE,
    VolumeUp = 0xAF,
    MediaNextTrack = 0xB0,
    MediaPreviousTrack = 0xB1,
    MediaStop = 0xB2,
    MediaPlayPause = 0xB3,
    LaunchMail = 0xB4,
    SelectMedia = 0xB5,
    LaunchApplication1 = 0xB6,
    LaunchApplication2 = 0xB7,
    OemSemicolon = 0xBA,
    Oem1 = OemSemicolon,
    Oemplus = 0xBB,
    Oemcomma = 0xBC,
    OemMinus = 0xBD,
    OemPeriod = 0xBE,
    OemQuestion = 0xBF,
    Oem2 = OemQuestion,
    Oemtilde = 0xC0,
    Oem3 = Oemtilde,
    OemOpenBrackets = 0xDB,
    Oem4 = OemOpenBrackets,
    OemPipe = 0xDC,
    Oem5 = OemPipe,
    OemCloseBrackets = 0xDD,
    Oem6 = OemCloseBrackets,
    OemQuotes = 0xDE,
    Oem7 = OemQuotes,
    Oem8 = 0xDF,
    OemBackslash = 0xE2,
    Oem102 = OemBackslash,
    ProcessKey = 0xE5,
    Packet = 0xE7,
    Attn = 0xF6,
    Crsel = 0xF7,
    Exsel = 0xF8,
    EraseEof = 0xF9,
    Play = 0xFA,
    Zoom = 0xFB,
    NoName = 0xFC,
    Pa1 = 0xFD,
    OemClear = 0xFE,
    Shift = 0x00010000,
    Control = 0x00020000,
    Alt = 0x00040000,
}
#endregion
public unsafe class Interop
{
    #region DLLImport
    const string user = "user32";
    const string kernel = "kernel32";

    [DllImport(user, CharSet = CharSet.Auto)] public static extern
        int MessageBox(nint hWnd, string text, string caption, uint type);

    [DllImport(user)] public static extern
        ushort GetAsyncKeyState(Keys key);

    [DllImport(user)] public static extern
        bool GetCursorInfo(ref CURSORINFO pci);

    [DllImport(user)] public static extern
        nint GetForegroundWindow();

    [DllImport(kernel, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)] public static extern 
        nint CreateFileW(string fileName, uint desAccess, uint shareMode, nint securityAttb, uint creationDispose, uint flagsAndAttb, nint templateFile);

    [DllImport(kernel)] [return: MarshalAs(UnmanagedType.Bool)] public static extern 
        bool AllocConsole();

    [DllImport(kernel)] public static extern
        uint GetTickCount();

    [DllImport(kernel, CharSet = CharSet.Unicode)] public static extern
        nint GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string moduleName);

    [DllImport(kernel, CharSet = CharSet.Ansi, ExactSpelling = true)] public static extern
        nint GetProcAddress(nint hModule, string procName);

    [DllImport(kernel)] public static extern
        nint GetCurrentThread();

    [DllImport(kernel, CharSet = CharSet.Unicode)] public static extern 
        nint LoadLibrary(string lpFileName);

    [DllImport(kernel)] public static extern
        nint LoadLibraryEx(string fileName, nint reservedNull, LoadLibraryFlags flags);

    [DllImport(kernel)] public static extern
        void Sleep(uint dwMilliseconds);

    [DllImport(kernel, CharSet = CharSet.Auto)] public static extern
        uint CreateThread(uint* threadAttributes, uint stackSize, ThreadStart startAddress, uint* parameter, uint creationFlags, out uint threadId);
    #endregion
    #region Method
    static int counter = 0;
    public static void Show(object message)
    {
        MessageBox(0, message == null ? "null" : message.ToString()!, counter.ToString(), 0);
        counter++;
    }

    public static int MessageBox(string text) => MessageBox(0, text, "", 0);
    public static int MessageBox(string caption, string text) => MessageBox(0, text, caption, 0);

    public static void* GetProcAddressPtr(nint hModule, [MarshalAs(UnmanagedType.LPWStr)] string lpModuleName) => (void*)GetProcAddress(hModule, lpModuleName);
    public static T* GetProcAddressPtr<T>(nint hModule, [MarshalAs(UnmanagedType.LPWStr)] string lpModuleName) where T : unmanaged => (T*)GetProcAddress(hModule, lpModuleName);

    public static uint StartThread(ThreadStart ThreadFunc)
    {
        uint i = 0;
        uint lpThreadID = 0;
        uint dwHandle = CreateThread(null, 0, ThreadFunc, &i, 0, out lpThreadID);
        return dwHandle;
    }

    public static bool IsCursorHide()
    {
        var cur = new CURSORINFO();
        cur.Size = sizeof(CURSORINFO);
        GetCursorInfo(ref cur);

        int realFlag = cur.Cursor.ToInt32();
        return realFlag > 66000 || realFlag < 65000;
    }

    public static bool IsWindowActive()
    {
        nint activeWindow = GetForegroundWindow();
        nint procWindow = Process.GetCurrentProcess().MainWindowHandle;
        return activeWindow == procWindow;
    }

    public static FileStream CreateFileStream(string name, uint desAccess, uint shareMode, FileAccess fileAccess)
    {
        var fileHandle = CreateFileW(name, desAccess, shareMode, 0, (uint)FileMode.Open, (uint)FileAttributes.Normal, 0);
        var file = new SafeFileHandle(fileHandle, true);
        return !file.IsInvalid ? new FileStream(file, fileAccess) : null;
    }   

    public static StreamWriter CreateOutStream() => new(CreateFileStream("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, FileAccess.Write)) { AutoFlush = true };

    public static StreamReader CreateInStream() => new(CreateFileStream("CONIN$", GENERIC_READ, FILE_SHARE_READ, FileAccess.Read));

    const uint
        GENERIC_WRITE = 0x40000000,
        GENERIC_READ = 0x80000000,
        FILE_SHARE_READ = 1,
        FILE_SHARE_WRITE = 2;

    #endregion
}