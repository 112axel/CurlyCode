using CurlyCode.Common.Classes;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CurlyCode.Lexer;

public static class Lexer
{
    public static List<Token> Run(StreamReader stream)
    {
        List<Token> tokens = new List<Token>();
        while (stream.Peek() != -1)
        {
            var token = GetToken(stream);
            tokens.Add(token);
        }
        return tokens;
    }


    private static Token GetToken(StreamReader stream)
    {
        string word = ReadWord(stream);
        //operations
        if (word == "+")
        {
            return new Token(Common.Enums.TokenType.Add, null);
        }
        if (word == "-")
        {
            return new Token(Common.Enums.TokenType.Subtract, null);
        }
        if (word == "/")
        {
            return new Token(Common.Enums.TokenType.Subtract, null);
        }
        if (word == "*")
        {
            return new Token(Common.Enums.TokenType.Subtract, null);
        }

        if (word == "=")
        {
            return new Token(Common.Enums.TokenType.Assignment, null);
        }

        if(word == ";")
        {
            return new Token(Common.Enums.TokenType.End, null);
        }

        if(word == "int")
        {
            return new Token(Common.Enums.TokenType.Type, word);
        }

        return new Token(Common.Enums.TokenType.Number, word);

    }

    private static string ReadWord(StreamReader stream)
    {
        //remove leading space
        while((char)stream.Peek() == ' ' && stream.Peek() != -1)
        {
            stream.Read();
        }

        StringBuilder stringBuilder = new StringBuilder();
        while((char)stream.Peek() != ' ' && stream.Peek() != -1)
        {
            stringBuilder.Append((char)stream.Read());
        }
        return stringBuilder.ToString();

    }

}


