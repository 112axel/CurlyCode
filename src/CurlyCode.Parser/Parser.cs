using CurlyCode.CodeGeneration;
using CurlyCode.Common.Enums;

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
        if (tokenList.Peek().TokenType == TokenType.Exit)
            if(tokenList.Peek(1).TokenType == TokenType.Number)
            {
                tokenList.ConsumeToken();
                var token = tokenList.ConsumeToken();
                SysCalls.Exit(writer,int.Parse(token.Data));
            }

        //int assignment
        if (tokenList.CheckForPattern(TokenType.NumberType, TokenType.Text, TokenType.Assignment, TokenType.Number))
        {
            //StackAbstraction.AddVariable()
        }

        //if (tokenList.Peek().TokenType == TokenType.Type)
        //    if (tokenList.Peek(1).TokenType == TokenType.Text)
        //        if (tokenList.Peek(2).TokenType == TokenType.Assignment)
        //            if (tokenList.Peek(3).TokenType == TokenType.Number)
        //            {
        //                writer.Write(' ');
        //            }



    }


}
