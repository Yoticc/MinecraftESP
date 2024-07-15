namespace Core;
public static class Logger
{
    [AllowNull] public static string Path;
    public static Encoding Encoding = Encoding.UTF8;

    [AllowNull] static FileStream stream;

    public static void StartNewSession(string fileName, string message)
    {
        SetFile(fileName);
        Clear();
        WriteLine(message);
    }

    public static void SetFile(string fileName) => stream = new FileStream(Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);

    public static void Clear() => stream.SetLength(0);

    public static void Write(object obj)
    {
        byte[] buffer = Encoding.GetBytes(obj.ToString()!);
        stream.Write(buffer, 0, buffer.Length);
        stream.Flush();
    }

    public static void WriteLine(object obj)
    {
        Write(obj);
        Write('\n');
    }
}