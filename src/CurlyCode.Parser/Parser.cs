using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Enums;
using CurlyCode.Common.Interfaces;
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

        if (tokenList.CheckForPattern(TokenType.Exit, TokenType.Number, TokenType.End))
        {
            tokenList.ConsumeToken();
            var token = tokenList.ConsumeToken();
            operations.Add(new Command("Exit", new AbsoluteStatement(int.Parse(token.Data))));
            //SysCalls.Exit(writer, int.Parse(token.Data));
            tokenList.ConsumeToken();
        }

        //int assignment
        if (tokenList.CheckForPattern(TokenType.NumberType, TokenType.Text, TokenType.Assignment, TokenType.Number, TokenType.End))
        {
            tokenList.ConsumeToken();
            var name = tokenList.ConsumeToken().Data;

            if (StackAbstraction.Exists(name))
            {
                throw new Exception("Variable \"{name}\" is already defined");
            }

            StackAbstraction.AddVariable(name);
            tokenList.ConsumeToken();
            //StackCode.AddValueToStack(writer, int.Parse(tokenList.ConsumeToken().Data));
            tokenList.ConsumeToken();
        }

        if (tokenList.CheckForPattern(TokenType.NumberType, TokenType.Text, TokenType.Assignment, TokenType.Text, TokenType.End))
        {

            //StackCode.GetValueFromStack(writer, 0);
        }

    }


}
