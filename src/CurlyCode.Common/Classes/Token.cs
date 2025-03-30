using CurlyCode.Common.Enums;

namespace CurlyCode.Common.Classes;

public record Token 
{
    public TokenType TokenType { get; set; }
    public string? Data { get; set; }

    public Token(TokenType tokenType, string? data = null)
    {
        TokenType = tokenType;
        Data = data;
    }
}
