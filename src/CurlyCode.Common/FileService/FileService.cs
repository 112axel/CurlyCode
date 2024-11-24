namespace CurlyCode.Common.FileService;

public static class FileService
{
    public static StreamReader GetStream(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"Path to file did not exist: {path}");
        }

        StreamReader stream = new StreamReader(path);
        return stream;
    }

}
