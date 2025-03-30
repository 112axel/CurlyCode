using CurlyCode.Common.Enums;
using CurlyCode.Parser;

namespace CurlyCode.Lexer.Tests;

[TestClass]
public sealed class TextSplitting
{
    private Stream MemStream;
    private void PrepareStream(string content)
    {
        MemStream = new MemoryStream();

        var writer = new StreamWriter(MemStream);
        writer.Write(content);
        writer.Flush();
        MemStream.Position = 0;
    }
    private static List<string> GetResult(string input)
    {
        using var memoryStream = new MemoryStream();

        var writer = new StreamWriter(memoryStream);
        writer.Write(input);
        writer.Flush();
        memoryStream.Position = 0;

        var result = new List<string>();

        var reader = new StreamReader(memoryStream);
        while (reader.Peek() != -1)
        {
            result.Add( StreamWordReader.ReadWord(reader));
        }
        return result;
    }

    private static bool CompareList(List<TokenType> expectedTokens, TokenList recivedTokens)
    {
        if (expectedTokens.Count != recivedTokens.Count)
        {
            return false;
        }

        for (int i = 0; i < expectedTokens.Count; i++)
        {
            if (expectedTokens[i] != recivedTokens[i].TokenType)
            {
                return false;
            }
        }
        return true;
    }

    private void Test(List<TokenType> answerKey, TokenList tokens)
    {
        Assert.IsTrue(tokens.Count == answerKey.Count, $"Wrong number of returns got {tokens.Count} expected {answerKey.Count}");
        Assert.IsTrue(CompareList(answerKey, tokens), "List did not match expected");
    }

    [TestMethod]
    public void OneLine()
    {
        var input = "exit(5);";
        List<TokenType> answerKey = [TokenType.Text, TokenType.OpenParentheses, TokenType.Number, TokenType.ClosedParentheses, TokenType.End];
        PrepareStream(input);
        var reader = new StreamReader(MemStream);
        var result = Lexer.Run(reader);

        Test(answerKey, result);
    }

    [TestMethod]
    public void LotsOfExtraSpace()
    {
        var input = "    exit    (    5         );    ";
        List<TokenType> answerKey = [TokenType.Text, TokenType.OpenParentheses, TokenType.Number, TokenType.ClosedParentheses, TokenType.End];

        PrepareStream(input);
        var reader = new StreamReader(MemStream);
        var result = Lexer.Run(reader);

        Test(answerKey,result);
    }

    [TestMethod]
    public void SplitLine()
    {
        var input = "exit (\n 5);";
        List<TokenType> answerKey = [TokenType.Text, TokenType.OpenParentheses, TokenType.Number, TokenType.ClosedParentheses, TokenType.End];

        PrepareStream(input);
        var reader = new StreamReader(MemStream);
        var result = Lexer.Run(reader);

        Test(answerKey,result);
    }

    [TestMethod]
    public void MathParse()
    {
        var input = "exit (5+4);";
        List<TokenType> answerKey = [TokenType.Text, TokenType.OpenParentheses, TokenType.Number,TokenType.Add,TokenType.Number, TokenType.ClosedParentheses, TokenType.End];

        PrepareStream(input);
        var reader = new StreamReader(MemStream);
        var result = Lexer.Run(reader);

        Test(answerKey,result);
    }

}
