namespace CurlyCode.Common.FileService;

public static class FileService
{
    public static StreamReader GetReadStream(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"Path to file did not exist: {path}");
        }

        StreamReader stream = new StreamReader(path);
        return stream;
    }

    public static StreamWriter GetStreamWriter(string path)
    {
        StreamWriter stream = new StreamWriter(path);
        return stream;
    }

}
