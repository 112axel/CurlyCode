using CurlyCode.Common.Classes;
using CurlyCode.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurlyCode.Parser.Tests;
[TestClass]
public class TokenListTests
{

    [TestMethod]
    public void SimpleMathStatement()
    {

        var list = new TokenList()
        {
            new Token(TokenType.Text,"exit"),
            new Token(TokenType.OpenParentheses),
            new Token(TokenType.Text,"4"),
            new Token(TokenType.Number,"4"),
            new Token(TokenType.Add),
            new Token(TokenType.Number,"4"),
            new Token(TokenType.ClosedParentheses),
            new Token(TokenType.End),
        };

        Assert.IsTrue(list.CheckForMathStatement(out var _));
    }

    [TestMethod]
    public void NotMathStatement()
    {

        var list = new TokenList()
        {
            new Token(TokenType.Number,"4"),
            new Token(TokenType.End),
            new Token(TokenType.End),
        };

        Assert.IsFalse(list.CheckForMathStatement(out var _));
    }

    [TestMethod]
    public void GetUntilEnd()
    {

        var list = new TokenList()
        {
            new Token(TokenType.Number,"4"),
            new Token(TokenType.End),
            new Token(TokenType.Number,"4"),
        };

        Assert.IsFalse(list.GetNextTokens().Count() == 2);
    }
}
