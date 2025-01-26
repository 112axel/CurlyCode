namespace CurlyCode.Parser.Statements;

internal class AbsoluteStatement : IStatement
{
    private readonly int Value;
    internal AbsoluteStatement(int value)
    {
        Value = value;
    }

    public int GetValue()
    {
        return Value;
    }
}
