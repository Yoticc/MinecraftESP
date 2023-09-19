namespace Core.Utils;
public static class Logger
{
    public static string Path { get; set; }
    public static Encoding Encoding { get; set; } = Encoding.UTF8;

    static FileStream stream;

    public static void SetFile(string path) => stream = new FileStream(Path = path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

    public static void Clear() => stream.SetLength(0);

    public static void Write(object obj)
    {
        byte[] buffer = Encoding.GetBytes(obj.ToString());
        stream.Write(buffer, 0, buffer.Length);
        stream.Flush();
    }

    public static void WriteLine(object obj)
    {
        Write(obj);
        Write('\n');
    }
}