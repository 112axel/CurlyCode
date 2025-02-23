using CurlyCode.Common.Interfaces;

namespace CurlyCode.Common.Classes.Operation;

public class BaseOperation : IOperation
{
    public List<IStatement> Statements { get; set; }
    public virtual int MaxStatements { get => 1000; }
    public string Identifier { get; }

    public BaseOperation(string identifier,IEnumerable<IStatement> statements)
    {
        Identifier = identifier;
        Statements = [.. statements];

        if (Statements.Count > MaxStatements)
        {
            throw new Exception("To many statements");
        }
    }
}
