using CurlyCode.Common.Interfaces;

namespace CurlyCode.Parser.Statements;

public class AbsoluteStatement : IStatement
{
    public readonly int Value;
    public AbsoluteStatement(int value)
    {
        Value = value;
    }
}
