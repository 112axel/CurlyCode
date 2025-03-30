using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Interfaces;

namespace CurlyCode.Parser.Operation;

public class Assignment : BaseOperation, IOperation
{
    public override int MaxStatements { get => 1; }
    public bool NewVariable;
    public Assignment(IStatement value, string name, bool newVariable = false) : base(name, [value])
    {
        NewVariable = newVariable;
    }

}
