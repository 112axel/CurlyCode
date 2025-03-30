using CurlyCode.Common.Classes;
using CurlyCode.Common.Enums;
using CurlyCode.Common.Interfaces;
using System.Reflection.Metadata.Ecma335;
namespace CurlyCode.Parser;

public class TokenList : List<Token>, IList<Token>
{
    public int TokenPlace { get; private set; } 
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
        var  a = this.ElementAtOrDefault(TokenPlace);
        this.RemoveAt(TokenPlace);
        return a;
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
    public bool CheckForMathStatement(out List<Token> resultTokens)
    {
        var operators = new List<TokenType> { TokenType.Add, TokenType.Subtract, TokenType.Divide, TokenType.Multiply };
        var terms = new List<TokenType> { TokenType.Number, TokenType.Text };

        resultTokens = new List<Token>();

        Token token;
        int count = 0;
        token = Peek();
        var containsOperator = false;
        while(token != null)
        {
            var expectingTerm = count % 2 == 0;
            if(token.TokenType == TokenType.ClosedParentheses)
            {
                if(count == 0)
                {
                    return false;
                }
                return !expectingTerm && containsOperator;
            }
            if (!IsParentheses(token))
            {
                if (expectingTerm)
                {
                    if (!terms.Contains(token.TokenType))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!operators.Contains(token.TokenType))
                    {
                        return false;
                    }
                    containsOperator = true;
                }
            }
            resultTokens.Add(token);
            count++;
            token = Peek(count);
        }

        return true;
    }

    private bool IsParentheses(Token token)
    {
        return token.TokenType == TokenType.OpenParentheses || token.TokenType == TokenType.ClosedParentheses;
    }


    public TokenList GetNextTokens()
    {
        TokenList output = [];
        var token = Peek();
        while(token != null && token.TokenType != TokenType.End)
        {
            output.Add(ConsumeToken());
            token = Peek();
        }
        if(token == null)
        {
            throw new Exception("No end token found");
        }

        output.Add(ConsumeToken());

        return output;
    }

    public IEnumerable<TokenList> GetLines()
    {
        var output = new List<TokenList>();
        while (true)
        {
            if (Peek() == null)
                break;
            var tokens = GetNextTokens();
            if (tokens.Count == 0)
                break;
            output.Add(tokens);
        }
        return output;
    }
}
