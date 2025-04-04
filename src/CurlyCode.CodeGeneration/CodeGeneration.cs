﻿using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Interfaces;
using CurlyCode.Parser;
using CurlyCode.Parser.Operation;
using CurlyCode.Parser.Statements;

namespace CurlyCode.CodeGeneration;

public static class CodeGeneration
{
    internal static FunctionRegister FunctionRegister { get; set; } = new();
    private static StreamWriter Writer { get; set; }

    static CodeGeneration()
    {
        //FunctionRegister.RegisterFunction("Exit", x => SysCalls.Exit())
    }
    private static void Start(this StreamWriter outStream)
    {
        outStream.WriteLine("global _start");
        outStream.WriteLine("_start: ");
        outStream.WriteLine("mov rbp, rsp");
    }


    public static void ParseOperations(IEnumerable<IOperation> operations, StreamWriter writer)
    {
        Writer = writer;
        Writer.Start();
        foreach(var operation in operations)
        {
            if (operation is Assignment assignment)
            {
                HandleAssignment(assignment);
            }
            else if (operation is Command)
            {
                HandleCommand(operation);
            }
        }
    }

    private static void HandleAssignment(Assignment assignment)
    {
        StackAbstraction.AddVariable(assignment.Identifier);
        if (assignment.Statements.First() is AbsoluteStatement absolute)
            StackCode.AddValueToStack(Writer, absolute.Value);
        else
            RunStatements(assignment.Statements);
    }

    private static void HandleCommand(IOperation operation)
    {
        if (operation.Identifier == "exit")
        {
            RunStatements(operation.Statements);
            SysCalls.Exit(Writer);
        }
    }

    private static void RunStatements(IEnumerable<IStatement> statements)
    {
        foreach (var statement in statements)
        {
            RunStatement(statement);
        }
    }

    private static void RunStatement(IStatement statement)
    {
        if (statement is AbsoluteStatement absolute)
        {
            StackCode.AddValueToStack(Writer, absolute.Value);
        }
        else if (statement is MathStatement math)
        {
            var a = math.GetExecutionPlan();
            var notation = math.ReversePolishNotation(a, null);
            var termNumber = 0;
            foreach (var node in notation)
            {
                if (node.Operation == MathStatement.Operation.TermNumber)
                {
                    RunStatement(math.Terms[termNumber]);
                    termNumber++;
                }
                else if(node.Operation == MathStatement.Operation.Add)
                {
                    Writer.Add();
                }
                else if(node.Operation == MathStatement.Operation.Sub)
                {
                    Writer.Subtract();
                }
                else if(node.Operation == MathStatement.Operation.Mult)
                {
                    Writer.Multiply();
                }
            }

        }
        else if (statement is VariableStatement variable)
        {
            var addr = StackAbstraction.GetAddress(variable.Identifier);
            Writer.PutOnTopOfStack(addr);
            //StackCode.GetValueFromStack(Writer, addr);
        }

    }

    private static bool IsMatch(this IOperation operation, string text)
    {
        return operation.Identifier == text;
    }
}
