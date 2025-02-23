using CurlyCode.Common.Enums;
namespace CurlyCode.Lexer.Tests;

[TestClass]
public sealed class LexerTests
{

    [TestMethod]
    public void NumberType()
    {
        var output = Lexer.ConvertTextToToken("int");

        Assert.AreEqual(output.TokenType, TokenType.NumberType, "Not the right token type");
    }

    [TestMethod]
    public void VariableName()
    {
        var input = "hello";
        var output = Lexer.ConvertTextToToken(input);

        Assert.AreEqual(output.TokenType, TokenType.Text, "Not the right token type");
        Assert.AreEqual(output.Data, input, "Not the right token type");
    }

    [TestMethod]
    public void Number()
    {
        var input = "112";
        var output = Lexer.ConvertTextToToken(input);

        Assert.AreEqual(output.TokenType, TokenType.Number, "Not the right token type");
        Assert.AreEqual(output.Data, input, "Not the right token type");
    }
}
