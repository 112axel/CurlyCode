using CurlyCode.Common.Interfaces;

namespace CurlyCode.Common.Classes.Operation;

public class Command : BaseOperation, IOperation
{
    public override int MaxStatements => base.MaxStatements;
    public Command(string name, params IEnumerable<IStatement> statements) : base(name, statements)
    {
    }

}
