using CurlyCode.Common.Classes;
using CurlyCode.Common.Enums;
namespace CurlyCode.Parser;

public class TokenList : List<Token>, IList<Token>
{
    private int TokenPlace; 
    public TokenList() { }

    internal Token? Peek(int count = 0)
    {
        if (count < 0)
            throw new ArgumentException("You cant peek negative places");

        if (TokenPlace>= Count )
            return null;

        return this[TokenPlace + count];
    }

    internal Token? ConsumeToken()
    {
        return this[TokenPlace++];
    }

    public bool CheckForPattern(params TokenType[] tokens)
    {
        for (int i = 0; i < tokens.Length; i++)
        {
            var token = Peek(i);
            if (token == null || token.TokenType != tokens[i])
            {
                return false;
            }
        }
        return true;
    }
}
