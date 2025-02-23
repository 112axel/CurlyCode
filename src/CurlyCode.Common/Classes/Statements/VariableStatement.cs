using CurlyCode.Common.Interfaces;

namespace CurlyCode.Parser.Statements;

public class VariableStatement : IStatement
{
    private readonly string varName;
    internal VariableStatement(string name)
    {
        varName = name;
    }

    public int GetValue()
    {
        //StackAbstraction.GetAddress(varName);
        //StackCode
        return 0;
    }
}
