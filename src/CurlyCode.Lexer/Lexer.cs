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
        return word switch
        {
            "+" => new Token(TokenType.Add, null),
            "-" => new Token(TokenType.Subtract, null),
            "/" => new Token(TokenType.Divide, null),
            "*" => new Token(TokenType.Multiply, null),
            "(" => new Token(TokenType.OpenParentheses),
            ")" => new Token(TokenType.ClosedParentheses),

            "=" => new Token(TokenType.Assignment, null),
            ";" => new Token(TokenType.End, null),
            "int" => new Token(TokenType.NumberType, word),
            //"exit" => new Token(TokenType.Function, null),

            _ => NumberOrText(word)
        };
    }


    private static Token NumberOrText(string word)
    {
        if (int.TryParse(word, out _))
        {
            return new Token(TokenType.Number, word);
        }

        return new Token(TokenType.Text, word);

    }

}


