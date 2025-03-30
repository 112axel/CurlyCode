using CurlyCode.Common.Interfaces;

namespace CurlyCode.Parser.Statements;

public class VariableStatement : IStatement
{
    public readonly string Identifier;
    public VariableStatement(string name)
    {
        Identifier = name;
    }
}
