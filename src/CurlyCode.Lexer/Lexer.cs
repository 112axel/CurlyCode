using CurlyCode.Common.Classes;
using CurlyCode.Common.Enums;
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
        string word = StreamWordReader.ReadWord(stream);
        //operations
        return ConvertTextToToken(word);
    }

    public static Token ConvertTextToToken(string word)
    {
        if (word == "+")
        {
            return new Token(TokenType.Add, null);
        }
        if (word == "-")
        {
            return new Token(TokenType.Subtract, null);
        }
        if (word == "/")
        {
            return new Token(TokenType.Divide, null);
        }
        if (word == "*")
        {
            return new Token(TokenType.Multiply, null);
        }

        if (word == "=")
        {
            return new Token(TokenType.Assignment, null);
        }

        if (word == ";")
        {
            return new Token(TokenType.End, null);
        }

        if (word == "int")
        {
            return new Token(TokenType.NumberType, word);
        }

        if (word == "exit")
        {
            return new Token(TokenType.Exit, null);
        }
        if (int.TryParse(word, out _))
        {
            return new Token(TokenType.Number, word);
        }

        return new Token(TokenType.Text, word);

    }

}


