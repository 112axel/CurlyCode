using CurlyCode.CodeGeneration;

namespace CurlyCode.Parser;

public static class Parser
{



    public static void Parse(TokenList tokens, StreamWriter outStream)
    {
        Start(outStream);
        AA(tokens,outStream);
    }

    private static void Start(StreamWriter outStream)
    {
        outStream.WriteLine("global _start");
        outStream.WriteLine("_start: ");
    }

    private static void AA(TokenList tokenList,StreamWriter writer)
    {
        if(tokenList.Peek().TokenType == Common.Enums.TokenType.Exit)
            if(tokenList.Peek(1).TokenType == Common.Enums.TokenType.Number)
            {
                tokenList.ConsumeToken();
                //tokenList.ConsumeToken();
                SysCalls.Exit(writer);
            }



        if (tokenList.Peek().TokenType == Common.Enums.TokenType.Type)
            if (tokenList.Peek(1).TokenType == Common.Enums.TokenType.Text)
                if (tokenList.Peek(2).TokenType == Common.Enums.TokenType.Assignment)
                    if (tokenList.Peek(3).TokenType == Common.Enums.TokenType.Number)
                    {
                        writer.Write(' ');
                    }



    }


}
