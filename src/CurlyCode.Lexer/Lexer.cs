using CurlyCode.Common.Classes;
using CurlyCode.Parser;
using System.Text;

namespace CurlyCode.Lexer;

public static class Lexer
{
    public static TokenList Run(StreamReader stream)
    {
        TokenList tokens = new TokenList();
        while (stream.Peek() != -1)
        {
            var token = GetToken(stream);
            tokens.Add(token);
        }
        return tokens;
    }


    private static Token GetToken(StreamReader stream)
    {
        string? word = StreamWordReader.ReadWord(stream);
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
            return new Token(Common.Enums.TokenType.NumberType, word);
        }

        if(word == "exit")
        {
            return new Token(Common.Enums.TokenType.Exit, null);
        }

        return new Token(Common.Enums.TokenType.Number, word);

    }


}


