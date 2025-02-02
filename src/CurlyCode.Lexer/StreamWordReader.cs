using System.Text;

namespace CurlyCode.Lexer;

public static class StreamWordReader
{
    public static string ReadWord(StreamReader stream)
    {
        var builder = new StringBuilder();

        ConsumeWhiteSpace(stream);
        while (true)
        {
            if(stream.Peek() == -1 )
            {
                break;
            }
            if(char.IsWhiteSpace((char)stream.Peek()))
            {
                break;
            }
            if((char)stream.Peek() == ';')
            {
                if(builder.Length == 0)
                {
                    builder.Append((char)stream.Read());
                }
                break;
            }
            builder.Append((char)stream.Read());
        }
        ConsumeWhiteSpace(stream);
        return builder.ToString();

    }

    private static void ConsumeWhiteSpace(StreamReader stream) {
        while (char.IsWhiteSpace((char)stream.Peek()) && stream.Peek() != -1)
        {
            stream.Read();
        }
    }
}
