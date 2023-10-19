namespace Core.Utils;
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

    public static ConsoleApp Open()
    {
        AllocConsole();
        return new();
    }

    public ConsoleApp SetupIn()
    {
        Console.SetIn(CreateInStream());
        return this;
    }

    public ConsoleApp SetupOut()
    {
        Console.SetOut(CreateOutStream());
        return this;
    }

    public ConsoleApp StartHandler()
    {
        StartThread(() =>
        {
            while (true)
            {
                try
                {
                    NewLine?.Invoke(Console.ReadLine());
                } catch { }
            }
        });

        return this;
    }

    public ConsoleApp Clear()
    {
        Console.Clear();
        return this;
    }
    public ConsoleApp Beep()
    {
        Console.Beep(500, 500);
        return this;
    }

    public delegate void NewLineHandler(string? line);
    public event NewLineHandler? NewLine;
}