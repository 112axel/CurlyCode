using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Interfaces;
using CurlyCode.Parser.Statements;

namespace CurlyCode.CodeGeneration;

public static class CodeGeneration
{
    internal static FunctionRegister FunctionRegister { get; set; } = new();

    static CodeGeneration()
    {
        //FunctionRegister.RegisterFunction("Exit", x => SysCalls.Exit())
    }
    private static void Start(this StreamWriter outStream)
    {
        outStream.WriteLine("global _start");
        outStream.WriteLine("_start: ");
    }


    public static void ParseOperations(IEnumerable<IOperation> operations, StreamWriter writer)
    {
        writer.Start();
        foreach(var operation in operations)
        {

            if(operation.Identifier == "Exit")
            {
                RunStatements(operation.Statements, writer);
                SysCalls.Exit(writer);
            }
        }
    }


    private static void RunStatements(IEnumerable<IStatement> statements, StreamWriter writer)
    {
        foreach(var statement in statements)
        {
            if(statement is AbsoluteStatement absolute)
            {
                StackCode.AddValueToStack(writer, absolute.Value);
            }
            else if(statement is MathStatement math)
            {

            }
            else if (statement is VariableStatement variable)
            {

            }
        }
    }
}
