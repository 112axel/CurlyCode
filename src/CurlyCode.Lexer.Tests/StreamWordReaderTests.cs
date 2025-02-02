namespace CurlyCode.Lexer.Tests;

[TestClass]
public sealed class StreamWordReaderTests
{

    private List<string> GetResult(string input)
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

    private bool CompareList(List<string> list1, List<string> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }
        return true;
    }

    private void Test(string input, List<string> answerKey)
    {
        var result = GetResult(input);

        Assert.IsTrue(result.Count == answerKey.Count, $"Wrong number of returns got {result.Count} expected {answerKey.Count}");
        Assert.IsTrue(CompareList(result, answerKey), "List did not match expected");
    }

    [TestMethod]
    public void OneLine()
    {
        var input = "exit 5;";
        List<string> answerKey = ["exit", "5", ";"];

        Test(input, answerKey);
    }

    [TestMethod]
    public void LotsOfExtraSpace()
    {
        List<string> answerKey = ["exit", "5", ";"];

        Test("exit    5       ;\t", answerKey);
    }

    [TestMethod]
    public void SplitLine()
    {
        List<string> answerKey = ["exit", "5", ";"];

        Test("exit \n 5;", answerKey);
    }
}
