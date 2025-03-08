using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Enums;
using CurlyCode.Common.Interfaces;
using CurlyCode.Parser.Operation;
using CurlyCode.Parser.Statements;

namespace CurlyCode.Parser;

public static class Parser
{

    public static List<IOperation> Parse(TokenList tokens)
    {
        var output = new List<IOperation>();
        output.AA(tokens);
        output.End();
        return output;
    }


    private static void End(this List<IOperation> operations)
    {
        operations.Add(new Command("Exit",new AbsoluteStatement(0)));
        //SysCalls.Exit(operations);
    }

    private static void AA(this List<IOperation> operations, TokenList tokenList)
    {
        var lastValue = -1;
        while (tokenList.Peek() != null)
        {
            if (lastValue == operations.Count)
                throw new Exception("Not able to use all Tokens");

            lastValue = operations.Count;
            if (tokenList.CheckForPattern(TokenType.Exit, TokenType.Number, TokenType.End))
            {
                tokenList.ConsumeToken();
                var token = tokenList.ConsumeToken();
                operations.Add(new Command("Exit", new AbsoluteStatement(int.Parse(token.Data))));
                tokenList.ConsumeToken();
            }
            else if (tokenList.CheckForPattern(TokenType.Exit, TokenType.Text, TokenType.End))
            {
                tokenList.ConsumeToken();
                var token = tokenList.ConsumeToken();
                operations.Add(new Command("Exit", new VariableStatement((token.Data))));
                tokenList.ConsumeToken();
            }
            //int assignment
            else if (tokenList.CheckForPattern(TokenType.NumberType, TokenType.Text, TokenType.Assignment, TokenType.Number, TokenType.End))
            {
                tokenList.ConsumeToken();
                var name = tokenList.ConsumeToken().Data;
                tokenList.ConsumeToken();
                var value = tokenList.ConsumeToken();

                operations.Add(new Assignment(new AbsoluteStatement(int.Parse(value.Data)), name));

                tokenList.ConsumeToken();
                //StackCode.AddValueToStack(writer, int.Parse(tokenList.ConsumeToken().Data));
            }
            else if (tokenList.CheckForPattern(TokenType.NumberType, TokenType.Text, TokenType.Assignment, TokenType.Text, TokenType.End))
            {

                //StackCode.GetValueFromStack(writer, 0);
            }

        }
    }


}
