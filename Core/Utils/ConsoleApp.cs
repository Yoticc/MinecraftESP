namespace Core;
public class ConsoleApp
{
    public static bool IsOpen()
    {
        try
        {
            Console.Write("");
            return true;
        }
        catch { }

        return false;
    }

    public void StartHandler()
    {
        StartThread(() =>
        {
            while (true)
                NewLine?.Invoke(Console.ReadLine());
        });
    }

    public void Clear() => Console.Clear();
    public void Beep() => Console.Beep(500, 500);

    public delegate void NewLineHandler(string line);
    public event NewLineHandler? NewLine;
}