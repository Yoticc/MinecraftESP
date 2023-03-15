using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Utils;
public unsafe class Interop
{
    [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    public static void* GetProcAddressPtr(IntPtr hModule, [MarshalAs(UnmanagedType.LPWStr)] string lpModuleName) => GetProcAddress(hModule, lpModuleName).ToPointer();

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

    [DllImport("kernel32")]
    public static extern IntPtr GetCurrentThread();

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

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

    [DllImport("kernel32")]
    public static extern void Sleep(uint dwMilliseconds);

    private static int counter = 0;
    public static void Show(object message)
    {
        MessageBox(0, message.ToString(), counter.ToString(), 0);
        counter++;
    }

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    private unsafe static extern uint CreateThread(uint* lpThreadAttributes, uint dwStackSize, ThreadStart lpStartAddress, uint* lpParameter, uint dwCreationFlags, out uint lpThreadId);

    public static uint StartThread(ThreadStart ThreadFunc)
    {
        uint i = 0;
        uint lpThreadID = 0;
        uint dwHandle = CreateThread(null, 0, ThreadFunc, &i, 0, out lpThreadID);
        return dwHandle;
    }

    [DllImport("user32")]
    public static extern ushort GetAsyncKeyState(Keys key);

    public enum Keys
    {

        ///  The bit mask to extract a key code from a key value.
        /// </summary>
        KeyCode = 0x0000FFFF,
        //  The bit mask to extract modifiers from a key value.
        /// </summary>
        Modifiers = unchecked((int)0xFFFF0000),
        //  No key pressed.
        /// </summary>
        None = 0x00,
        //  The left mouse button.
        /// </summary>
        LButton = 0x01,
        //  The right mouse button.
        /// </summary>
        RButton = 0x02,
        //  The CANCEL key.
        /// </summary>
        Cancel = 0x03,
        //  The middle mouse button (three-button mouse).
        /// </summary>
        MButton = 0x04,
        //  The first x mouse button (five-button mouse).
        /// </summary>
        XButton1 = 0x05,
        //  The second x mouse button (five-button mouse).
        /// </summary>
        XButton2 = 0x06,
        //  The BACKSPACE key.
        /// </summary>
        Back = 0x08,
        //  The TAB key.
        /// </summary>
        Tab = 0x09,
        //  The CLEAR key.
        /// </summary>
        LineFeed = 0x0A,
        //  The CLEAR key.
        /// </summary>
        Clear = 0x0C,
        //  The RETURN key.
        /// </summary>
        Return = 0x0D,
        //  The ENTER key.
        /// </summary>
        Enter = Return,
        //  The SHIFT key.
        /// </summary>
        ShiftKey = 0x10,
        //  The CTRL key.
        /// </summary>
        ControlKey = 0x11,
        //  The ALT key.
        /// </summary>
        Menu = 0x12,
        //  The PAUSE key.
        /// </summary>
        Pause = 0x13,
        //  The CAPS LOCK key.
        /// </summary>
        Capital = 0x14,
        //  The CAPS LOCK key.
        /// </summary>
        CapsLock = 0x14,
        //  The IME Kana mode key.
        /// </summary>
        KanaMode = 0x15,
        //  The IME Hanguel mode key.
        /// </summary>
        HanguelMode = 0x15,
        //  The IME Hangul mode key.
        /// </summary>
        HangulMode = 0x15,
        //  The IME Junja mode key.
        /// </summary>
        JunjaMode = 0x17,
        //  The IME Final mode key.
        /// </summary>
        FinalMode = 0x18,
        //  The IME Hanja mode key.
        /// </summary>
        HanjaMode = 0x19,
        //  The IME Kanji mode key.
        /// </summary>
        KanjiMode = 0x19,
        //  The ESC key.
        /// </summary>
        Escape = 0x1B,
        //  The IME Convert key.
        /// </summary>
        IMEConvert = 0x1C,
        //  The IME NonConvert key.
        /// </summary>
        IMENonconvert = 0x1D,
        //  The IME Accept key.
        /// </summary>
        IMEAccept = 0x1E,
        //  The IME Accept key.
        /// </summary>
        IMEAceept = IMEAccept,
        //  The IME Mode change request.
        /// </summary>
        IMEModeChange = 0x1F,
        //  The SPACEBAR key.
        /// </summary>
        Space = 0x20,
        //  The PAGE UP key.
        /// </summary>
        Prior = 0x21,
        //  The PAGE UP key.
        /// </summary>
        PageUp = Prior,
        //  The PAGE DOWN key.
        /// </summary>
        Next = 0x22,
        //  The PAGE DOWN key.
        /// </summary>
        PageDown = Next,
        //  The END key.
        /// </summary>
        End = 0x23,
        //  The HOME key.
        /// </summary>
        Home = 0x24,
        //  The LEFT ARROW key.
        /// </summary>
        Left = 0x25,
        //  The UP ARROW key.
        /// </summary>
        Up = 0x26,
        //  The RIGHT ARROW key.
        /// </summary>
        Right = 0x27,
        //  The DOWN ARROW key.
        /// </summary>
        Down = 0x28,
        //  The SELECT key.
        /// </summary>
        Select = 0x29,
        //  The PRINT key.
        /// </summary>
        Print = 0x2A,
        //  The EXECUTE key.
        /// </summary>
        Execute = 0x2B,
        //  The PRINT SCREEN key.
        /// </summary>
        Snapshot = 0x2C,
        //  The PRINT SCREEN key.
        /// </summary>
        PrintScreen = Snapshot,
        //  The INS key.
        /// </summary>
        Insert = 0x2D,
        //  The DEL key.
        /// </summary>
        Delete = 0x2E,
        //  The HELP key.
        /// </summary>
        Help = 0x2F,
        //  The 0 key.
        /// </summary>
        D0 = 0x30, // 0
                   //  The 1 key.
        /// </summary>
        D1 = 0x31, // 1
                   //  The 2 key.
        /// </summary>
        D2 = 0x32, // 2
                   //  The 3 key.
        /// </summary>
        D3 = 0x33, // 3
                   //  The 4 key.
        /// </summary>
        D4 = 0x34, // 4
                   //  The 5 key.
        /// </summary>
        D5 = 0x35, // 5
                   //  The 6 key.
        /// </summary>
        D6 = 0x36, // 6
                   //  The 7 key.
        /// </summary>
        D7 = 0x37, // 7
                   //  The 8 key.
        /// </summary>
        D8 = 0x38, // 8
                   //  The 9 key.
        /// </summary>
        D9 = 0x39, // 9
                   //  The A key.
        /// </summary>
        A = 0x41,
        //  The B key.
        /// </summary>
        B = 0x42,
        //  The C key.
        /// </summary>
        C = 0x43,
        //  The D key.
        /// </summary>
        D = 0x44,
        //  The E key.
        /// </summary>
        E = 0x45,
        //  The F key.
        /// </summary>
        F = 0x46,
        //  The G key.
        /// </summary>
        G = 0x47,
        //  The H key.
        /// </summary>
        H = 0x48,
        //  The I key.
        /// </summary>
        I = 0x49,
        //  The J key.
        /// </summary>
        J = 0x4A,
        //  The K key.
        /// </summary>
        K = 0x4B,
        //  The L key.
        /// </summary>
        L = 0x4C,
        //  The M key.
        /// </summary>
        M = 0x4D,
        //  The N key.
        /// </summary>
        N = 0x4E,
        //  The O key.
        /// </summary>
        O = 0x4F,
        //  The P key.
        /// </summary>
        P = 0x50,
        //  The Q key.
        /// </summary>
        Q = 0x51,
        //  The R key.
        /// </summary>
        R = 0x52,
        //  The S key.
        /// </summary>
        S = 0x53,
        //  The T key.
        /// </summary>
        T = 0x54,
        //  The U key.
        /// </summary>
        U = 0x55,
        //  The V key.
        /// </summary>
        V = 0x56,
        //  The W key.
        /// </summary>
        W = 0x57,
        //  The X key.
        /// </summary>
        X = 0x58,
        //  The Y key.
        /// </summary>
        Y = 0x59,
        //  The Z key.
        /// </summary>
        Z = 0x5A,
        //  The left Windows logo key (Microsoft Natural Keyboard).
        /// </summary>
        LWin = 0x5B,
        //  The right Windows logo key (Microsoft Natural Keyboard).
        /// </summary>
        RWin = 0x5C,
        //  The Application key (Microsoft Natural Keyboard).
        /// </summary>
        Apps = 0x5D,
        //  The Computer Sleep key.
        /// </summary>
        Sleep = 0x5F,
        //  The 0 key on the numeric keypad.
        /// </summary>
        NumPad0 = 0x60,
        //  The 1 key on the numeric keypad.
        /// </summary>
        NumPad1 = 0x61,
        //  The 2 key on the numeric keypad.
        /// </summary>
        NumPad2 = 0x62,
        //  The 3 key on the numeric keypad.
        /// </summary>
        NumPad3 = 0x63,
        //  The 4 key on the numeric keypad.
        /// </summary>
        NumPad4 = 0x64,
        //  The 5 key on the numeric keypad.
        /// </summary>
        NumPad5 = 0x65,
        //  The 6 key on the numeric keypad.
        /// </summary>
        NumPad6 = 0x66,
        //  The 7 key on the numeric keypad.
        /// </summary>
        NumPad7 = 0x67,
        //  The 8 key on the numeric keypad.
        /// </summary>
        NumPad8 = 0x68,
        //  The 9 key on the numeric keypad.
        /// </summary>
        NumPad9 = 0x69,
        //  The Multiply key.
        /// </summary>
        Multiply = 0x6A,
        //  The Add key.
        /// </summary>
        Add = 0x6B,
        //  The Separator key.
        /// </summary>
        Separator = 0x6C,
        //  The Subtract key.
        /// </summary>
        Subtract = 0x6D,
        //  The Decimal key.
        /// </summary>
        Decimal = 0x6E,
        //  The Divide key.
        /// </summary>
        Divide = 0x6F,
        //  The F1 key.
        /// </summary>
        F1 = 0x70,
        //  The F2 key.
        /// </summary>
        F2 = 0x71,
        //  The F3 key.
        /// </summary>
        F3 = 0x72,
        //  The F4 key.
        /// </summary>
        F4 = 0x73,
        //  The F5 key.
        /// </summary>
        F5 = 0x74,
        //  The F6 key.
        /// </summary>
        F6 = 0x75,
        //  The F7 key.
        /// </summary>
        F7 = 0x76,
        //  The F8 key.
        /// </summary>
        F8 = 0x77,
        //  The F9 key.
        /// </summary>
        F9 = 0x78,
        //  The F10 key.
        /// </summary>
        F10 = 0x79,
        //  The F11 key.
        /// </summary>
        F11 = 0x7A,
        //  The F12 key.
        /// </summary>
        F12 = 0x7B,
        //  The F13 key.
        /// </summary>
        F13 = 0x7C,
        //  The F14 key.
        /// </summary>
        F14 = 0x7D,
        //  The F15 key.
        /// </summary>
        F15 = 0x7E,
        //  The F16 key.
        /// </summary>
        F16 = 0x7F,
        //  The F17 key.
        /// </summary>
        F17 = 0x80,
        //  The F18 key.
        /// </summary>
        F18 = 0x81,
        //  The F19 key.
        /// </summary>
        F19 = 0x82,
        //  The F20 key.
        /// </summary>
        F20 = 0x83,
        //  The F21 key.
        /// </summary>
        F21 = 0x84,
        //  The F22 key.
        /// </summary>
        F22 = 0x85,
        //  The F23 key.
        /// </summary>
        F23 = 0x86,
        //  The F24 key.
        /// </summary>
        F24 = 0x87,
        //  The NUM LOCK key.
        /// </summary>
        NumLock = 0x90,
        //  The SCROLL LOCK key.
        /// </summary>
        Scroll = 0x91,
        //  The left SHIFT key.
        /// </summary>
        LShiftKey = 0xA0,
        //  The right SHIFT key.
        /// </summary>
        RShiftKey = 0xA1,
        //  The left CTRL key.
        /// </summary>
        LControlKey = 0xA2,
        //  The right CTRL key.
        /// </summary>
        RControlKey = 0xA3,
        //  The left ALT key.
        /// </summary>
        LMenu = 0xA4,
        //  The right ALT key.
        /// </summary>
        RMenu = 0xA5,
        //  The Browser Back key.
        /// </summary>
        BrowserBack = 0xA6,
        //  The Browser Forward key.
        /// </summary>
        BrowserForward = 0xA7,
        //  The Browser Refresh key.
        /// </summary>
        BrowserRefresh = 0xA8,
        //  The Browser Stop key.
        /// </summary>
        BrowserStop = 0xA9,
        //  The Browser Search key.
        /// </summary>
        BrowserSearch = 0xAA,
        //  The Browser Favorites key.
        /// </summary>
        BrowserFavorites = 0xAB,
        //  The Browser Home key.
        /// </summary>
        BrowserHome = 0xAC,
        //  The Volume Mute key.
        /// </summary>
        VolumeMute = 0xAD,
        //  The Volume Down key.
        /// </summary>
        VolumeDown = 0xAE,
        //  The Volume Up key.
        /// </summary>
        VolumeUp = 0xAF,
        //  The Media Next Track key.
        /// </summary>
        MediaNextTrack = 0xB0,
        //  The Media Previous Track key.
        /// </summary>
        MediaPreviousTrack = 0xB1,
        //  The Media Stop key.
        /// </summary>
        MediaStop = 0xB2,
        //  The Media Play Pause key.
        /// </summary>
        MediaPlayPause = 0xB3,
        //  The Launch Mail key.
        /// </summary>
        LaunchMail = 0xB4,
        //  The Select Media key.
        /// </summary>
        SelectMedia = 0xB5,
        //  The Launch Application1 key.
        /// </summary>
        LaunchApplication1 = 0xB6,
        //  The Launch Application2 key.
        /// </summary>
        LaunchApplication2 = 0xB7,
        //  The Oem Semicolon key.
        /// </summary>
        OemSemicolon = 0xBA,
        //  The Oem 1 key.
        /// </summary>
        Oem1 = OemSemicolon,
        //  The Oem plus key.
        /// </summary>
        Oemplus = 0xBB,
        //  The Oem comma key.
        /// </summary>
        Oemcomma = 0xBC,
        //  The Oem Minus key.
        /// </summary>
        OemMinus = 0xBD,
        //  The Oem Period key.
        /// </summary>
        OemPeriod = 0xBE,
        //  The Oem Question key.
        /// </summary>
        OemQuestion = 0xBF,
        //  The Oem 2 key.
        /// </summary>
        Oem2 = OemQuestion,
        //  The Oem tilde key.
        /// </summary>
        Oemtilde = 0xC0,
        //  The Oem 3 key.
        /// </summary>
        Oem3 = Oemtilde,
        //  The Oem Open Brackets key.
        /// </summary>
        OemOpenBrackets = 0xDB,
        //  The Oem 4 key.
        /// </summary>
        Oem4 = OemOpenBrackets,
        //  The Oem Pipe key.
        /// </summary>
        OemPipe = 0xDC,
        //  The Oem 5 key.
        /// </summary>
        Oem5 = OemPipe,
        //  The Oem Close Brackets key.
        /// </summary>
        OemCloseBrackets = 0xDD,
        //  The Oem 6 key.
        /// </summary>
        Oem6 = OemCloseBrackets,
        //  The Oem Quotes key.
        /// </summary>
        OemQuotes = 0xDE,
        //  The Oem 7 key.
        /// </summary>
        Oem7 = OemQuotes,
        //  The Oem8 key.
        /// </summary>
        Oem8 = 0xDF,
        //  The Oem Backslash key.
        /// </summary>
        OemBackslash = 0xE2,
        //  The Oem 102 key.
        /// </summary>
        Oem102 = OemBackslash,
        //  The PROCESS KEY key.
        /// </summary>
        ProcessKey = 0xE5,
        //  The Packet KEY key.
        /// </summary>
        Packet = 0xE7,
        //  The ATTN key.
        /// </summary>
        Attn = 0xF6,
        //  The CRSEL key.
        /// </summary>
        Crsel = 0xF7,
        //  The EXSEL key.
        /// </summary>
        Exsel = 0xF8,
        //  The ERASE EOF key.
        /// </summary>
        EraseEof = 0xF9,
        //  The PLAY key.
        /// </summary>
        Play = 0xFA,
        //  The ZOOM key.
        /// </summary>
        Zoom = 0xFB,
        //  A constant reserved for future use.
        /// </summary>
        NoName = 0xFC,
        //  The PA1 key.
        /// </summary>
        Pa1 = 0xFD,
        //  The CLEAR key.
        /// </summary>
        OemClear = 0xFE,
        //  The SHIFT modifier key.
        /// </summary>
        Shift = 0x00010000,
        //  The  CTRL modifier key.
        /// </summary>
        Control = 0x00020000,
        //  The ALT modifier key.

        Alt = 0x00040000,
    }
}