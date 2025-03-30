using CurlyCode.Common.Classes;
using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Enums;
using CurlyCode.Common.Interfaces;
using CurlyCode.Parser.Operation;
using CurlyCode.Parser.Statements;
using System.Dynamic;

namespace CurlyCode.Parser;

public static class Parser
{

    public static List<IOperation> Parse(TokenList tokens)
    {
        var output = new List<IOperation>();

        var lines = tokens.GetLines();

        foreach (var line in lines)
        {
            output.ParseLine(line);
        }

        output.End();
        return output;
    }


    private static void End(this List<IOperation> operations)
    {
        operations.Add(new Command("Exit",new AbsoluteStatement(0)));
        //SysCalls.Exit(operations);
    }

    private static void ParseLine(this List<IOperation> operations, TokenList tokenList)
    {

            if (tokenList.CheckForPattern(TokenType.Text,TokenType.OpenParentheses))
            {
                var function = tokenList.ConsumeToken();
                //var statement = GetStatement(tokenList);
                operations.Add(new Command(function.Data, GetFunctionStatements(tokenList)));
                tokenList.ConsumeToken();
            }
            else if (tokenList.CheckForPattern(TokenType.NumberType,TokenType.Text, TokenType.Assignment)|| tokenList.CheckForPattern(TokenType.Text, TokenType.Assignment))//variable assignment 
            {
                var isDeclaration = false;
                if(tokenList.Peek().TokenType == TokenType.NumberType)
                {
                    isDeclaration = true;
                    tokenList.ConsumeToken();
                }
                var variableName = tokenList.ConsumeToken();
                tokenList.ConsumeToken();
                operations.Add(new Assignment(GetStatement(tokenList),variableName.Data,isDeclaration));

            }
    }

    private static List<IStatement> GetFunctionStatements(TokenList tokens)
    {
        var statements = new List<IStatement>();
        var value = GetInsideParenthesis(tokens);

        statements.Add(GetStatement(value));
        return statements;
    }

    private static TokenList GetInsideParenthesis(TokenList tokens)
    {
        var output = new TokenList();
        int open = 0;
        foreach (var token in tokens)
        {
            if(token.TokenType == TokenType.OpenParentheses)
                open++;
            if(token.TokenType == TokenType.ClosedParentheses)
                open--;
            output.Add(token);
            if(open == 0)
            {
                return output;
            }
        }
        throw new Exception("Could not escape parenthesis");
    }

    private static IStatement GetStatement(TokenList tokens)
    {
        if (tokens.CheckForPattern(TokenType.Text, TokenType.End))
        {
            return new VariableStatement("a");
        }
        else if (tokens.CheckForPattern(TokenType.Number, TokenType.End))
        {
            return new AbsoluteStatement(1);
        }
        else
        {

        }
        var terms = new List<IStatement>();

        foreach (var token in tokens)
        {
            if (token.TokenType == TokenType.Number)
            {
                terms.Add(new AbsoluteStatement(int.Parse(token.Data)));
            }
            else if (token.TokenType == TokenType.Text)
            {
                throw new NotImplementedException("Get statement variables not implented");
                //terms.Add((IStatement)token);
            }
            //tokens.ConsumeToken();
        }

        return new MathStatement(tokens) { Terms = terms };

        throw new Exception("Could not find statement after");
    }

}
