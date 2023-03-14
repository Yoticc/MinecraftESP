﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESP;
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
        /// </summary>
        Modifiers = unchecked((int)0xFFFF0000),
        /// </summary>
        None = 0x00,
        /// </summary>
        LButton = 0x01,
        /// </summary>
        RButton = 0x02,
        /// </summary>
        Cancel = 0x03,
        /// </summary>
        MButton = 0x04,
        /// </summary>
        XButton1 = 0x05,
        /// </summary>
        XButton2 = 0x06,
        /// </summary>
        Back = 0x08,
        /// </summary>
        Tab = 0x09,
        /// </summary>
        LineFeed = 0x0A,
        /// </summary>
        Clear = 0x0C,
        /// </summary>
        Return = 0x0D,
        /// </summary>
        Enter = Return,
        /// </summary>
        ShiftKey = 0x10,
        /// </summary>
        ControlKey = 0x11,
        /// </summary>
        Menu = 0x12,
        /// </summary>
        Pause = 0x13,
        /// </summary>
        Capital = 0x14,
        /// </summary>
        CapsLock = 0x14,
        /// </summary>
        KanaMode = 0x15,
        /// </summary>
        HanguelMode = 0x15,
        /// </summary>
        HangulMode = 0x15,
        /// </summary>
        JunjaMode = 0x17,
        /// </summary>
        FinalMode = 0x18,
        /// </summary>
        HanjaMode = 0x19,
        /// </summary>
        KanjiMode = 0x19,
        /// </summary>
        Escape = 0x1B,
        /// </summary>
        IMEConvert = 0x1C,
        /// </summary>
        IMENonconvert = 0x1D,
        /// </summary>
        IMEAccept = 0x1E,
        /// </summary>
        IMEAceept = IMEAccept,
        /// </summary>
        IMEModeChange = 0x1F,
        /// </summary>
        Space = 0x20,
        /// </summary>
        Prior = 0x21,
        /// </summary>
        PageUp = Prior,
        /// </summary>
        Next = 0x22,
        /// </summary>
        PageDown = Next,
        /// </summary>
        End = 0x23,
        /// </summary>
        Home = 0x24,
        /// </summary>
        Left = 0x25,
        /// </summary>
        Up = 0x26,
        /// </summary>
        Right = 0x27,
        /// </summary>
        Down = 0x28,
        /// </summary>
        Select = 0x29,
        /// </summary>
        Print = 0x2A,
        /// </summary>
        Execute = 0x2B,
        /// </summary>
        Snapshot = 0x2C,
        /// </summary>
        PrintScreen = Snapshot,
        /// </summary>
        Insert = 0x2D,
        /// </summary>
        Delete = 0x2E,
        /// </summary>
        Help = 0x2F,
        /// </summary>
        D0 = 0x30, // 0
        /// </summary>
        D1 = 0x31, // 1
        /// </summary>
        D2 = 0x32, // 2
        /// </summary>
        D3 = 0x33, // 3
        /// </summary>
        D4 = 0x34, // 4
        /// </summary>
        D5 = 0x35, // 5
        /// </summary>
        D6 = 0x36, // 6
        /// </summary>
        D7 = 0x37, // 7
        /// </summary>
        D8 = 0x38, // 8
        /// </summary>
        D9 = 0x39, // 9
        /// </summary>
        A = 0x41,
        /// </summary>
        B = 0x42,
        /// </summary>
        C = 0x43,
        /// </summary>
        D = 0x44,
        /// </summary>
        E = 0x45,
        /// </summary>
        F = 0x46,
        /// </summary>
        G = 0x47,
        /// </summary>
        H = 0x48,
        /// </summary>
        I = 0x49,
        /// </summary>
        J = 0x4A,
        /// </summary>
        K = 0x4B,
        /// </summary>
        L = 0x4C,
        /// </summary>
        M = 0x4D,
        /// </summary>
        N = 0x4E,
        /// </summary>
        O = 0x4F,
        /// </summary>
        P = 0x50,
        /// </summary>
        Q = 0x51,
        /// </summary>
        R = 0x52,
        /// </summary>
        S = 0x53,
        /// </summary>
        T = 0x54,
        /// </summary>
        U = 0x55,
        /// </summary>
        V = 0x56,
        /// </summary>
        W = 0x57,
        /// </summary>
        X = 0x58,
        /// </summary>
        Y = 0x59,
        /// </summary>
        Z = 0x5A,
        /// </summary>
        LWin = 0x5B,
        /// </summary>
        RWin = 0x5C,
        /// </summary>
        Apps = 0x5D,
        /// </summary>
        Sleep = 0x5F,
        /// </summary>
        NumPad0 = 0x60,
        /// </summary>
        NumPad1 = 0x61,
        /// </summary>
        NumPad2 = 0x62,
        /// </summary>
        NumPad3 = 0x63,
        /// </summary>
        NumPad4 = 0x64,
        /// </summary>
        NumPad5 = 0x65,
        /// </summary>
        NumPad6 = 0x66,
        /// </summary>
        NumPad7 = 0x67,
        /// </summary>
        NumPad8 = 0x68,
        /// </summary>
        NumPad9 = 0x69,
        /// </summary>
        Multiply = 0x6A,
        /// </summary>
        Add = 0x6B,
        /// </summary>
        Separator = 0x6C,
        /// </summary>
        Subtract = 0x6D,
        /// </summary>
        Decimal = 0x6E,
        /// </summary>
        Divide = 0x6F,
        /// </summary>
        F1 = 0x70,
        /// </summary>
        F2 = 0x71,
        /// </summary>
        F3 = 0x72,
        /// </summary>
        F4 = 0x73,
        /// </summary>
        F5 = 0x74,
        /// </summary>
        F6 = 0x75,
        /// </summary>
        F7 = 0x76,
        /// </summary>
        F8 = 0x77,
        /// </summary>
        F9 = 0x78,
        /// </summary>
        F10 = 0x79,
        /// </summary>
        F11 = 0x7A,
        /// </summary>
        F12 = 0x7B,
        /// </summary>
        F13 = 0x7C,
        /// </summary>
        F14 = 0x7D,
        /// </summary>
        F15 = 0x7E,
        /// </summary>
        F16 = 0x7F,
        /// </summary>
        F17 = 0x80,
        /// </summary>
        F18 = 0x81,
        /// </summary>
        F19 = 0x82,
        /// </summary>
        F20 = 0x83,
        /// </summary>
        F21 = 0x84,
        /// </summary>
        F22 = 0x85,
        /// </summary>
        F23 = 0x86,
        /// </summary>
        F24 = 0x87,
        /// </summary>
        NumLock = 0x90,
        /// </summary>
        Scroll = 0x91,
        /// </summary>
        LShiftKey = 0xA0,
        /// </summary>
        RShiftKey = 0xA1,
        /// </summary>
        LControlKey = 0xA2,
        /// </summary>
        RControlKey = 0xA3,
        /// </summary>
        LMenu = 0xA4,
        /// </summary>
        RMenu = 0xA5,
        /// </summary>
        BrowserBack = 0xA6,
        /// </summary>
        BrowserForward = 0xA7,
        /// </summary>
        BrowserRefresh = 0xA8,
        /// </summary>
        BrowserStop = 0xA9,
        /// </summary>
        BrowserSearch = 0xAA,
        /// </summary>
        BrowserFavorites = 0xAB,
        /// </summary>
        BrowserHome = 0xAC,
        /// </summary>
        VolumeMute = 0xAD,
        /// </summary>
        VolumeDown = 0xAE,
        /// </summary>
        VolumeUp = 0xAF,
        /// </summary>
        MediaNextTrack = 0xB0,
        /// </summary>
        MediaPreviousTrack = 0xB1,
        /// </summary>
        MediaStop = 0xB2,
        /// </summary>
        MediaPlayPause = 0xB3,
        /// </summary>
        LaunchMail = 0xB4,
        /// </summary>
        SelectMedia = 0xB5,
        /// </summary>
        LaunchApplication1 = 0xB6,
        /// </summary>
        LaunchApplication2 = 0xB7,
        /// </summary>
        OemSemicolon = 0xBA,
        /// </summary>
        Oem1 = OemSemicolon,
        /// </summary>
        Oemplus = 0xBB,
        /// </summary>
        Oemcomma = 0xBC,
        /// </summary>
        OemMinus = 0xBD,
        /// </summary>
        OemPeriod = 0xBE,
        /// </summary>
        OemQuestion = 0xBF,
        /// </summary>
        Oem2 = OemQuestion,
        /// </summary>
        Oemtilde = 0xC0,
        /// </summary>
        Oem3 = Oemtilde,
        /// </summary>
        OemOpenBrackets = 0xDB,
        /// </summary>
        Oem4 = OemOpenBrackets,
        /// </summary>
        OemPipe = 0xDC,
        /// </summary>
        Oem5 = OemPipe,
        /// </summary>
        OemCloseBrackets = 0xDD,
        /// </summary>
        Oem6 = OemCloseBrackets,
        /// </summary>
        OemQuotes = 0xDE,
        /// </summary>
        Oem7 = OemQuotes,
        /// </summary>
        Oem8 = 0xDF,
        /// </summary>
        OemBackslash = 0xE2,
        /// </summary>
        Oem102 = OemBackslash,
        /// </summary>
        ProcessKey = 0xE5,
        /// </summary>
        Packet = 0xE7,
        /// </summary>
        Attn = 0xF6,
        /// </summary>
        Crsel = 0xF7,
        /// </summary>
        Exsel = 0xF8,
        /// </summary>
        EraseEof = 0xF9,
        /// </summary>
        Play = 0xFA,
        /// </summary>
        Zoom = 0xFB,
        /// </summary>
        NoName = 0xFC,
        /// </summary>
        Pa1 = 0xFD,
        /// </summary>
        OemClear = 0xFE,
        /// </summary>
        Shift = 0x00010000,
        /// </summary>
        Control = 0x00020000,

        Alt = 0x00040000,
    }
}