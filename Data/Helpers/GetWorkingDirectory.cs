using System.Runtime.CompilerServices;

namespace api.Data.Helpers;

public static class Directory
{
    public static string GetWorkingDirectory()
    {
        string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        
        string? path = Path.GetDirectoryName(strExeFilePath);

        if (path == null)
        {
            throw new DirectoryNotFoundException();
        }

        return path;
    }

    public static bool DirectoryExists(string path)
    {
        return Path.Exists(Path.Combine(GetWorkingDirectory(), path));
    }

    public static DirectoryInfo CreateDirectory(string path)
    {
        return System.IO.Directory.CreateDirectory(Path.Combine(GetWorkingDirectory(), path));
    }

    public static string GetDirectory(string path)
    {
        return Path.Combine(GetWorkingDirectory(), path);
    }
}