namespace Core.Utils;
public static class ModulesInfo
{
    static ModulesInfo()
    {
        try
        {
            var collection = Process.GetCurrentProcess().Modules.GetEnumerator();
            while (collection.MoveNext())
            {
                var moduleObj = collection.Current;

                if (moduleObj == null)
                    continue;

                var module = (moduleObj as ProcessModule)!;

                var path = module.FileName;
                var name = Path.GetFileNameWithoutExtension(path);
                var lowName = name.ToLower();
                var handle = module.BaseAddress;
                Modules.Add(new(handle, path, lowName, lowName));
            }
        }
        catch // At some time, this may return a random Exception, so we pray to Allah and set try-catch
        {

        }
    }

    public static List<Module> Modules = [];

    public static Module? GetModuleByName(string name)
    {
        var lowName = name.ToLower();
        return Modules.Find(m => m.LowName == lowName);
    }
}

public record Module(nint Handle, string Path, string Name, string LowName);