using CurlyCode.Common.Classes.Operation;
using CurlyCode.Common.Interfaces;

namespace CurlyCode.Parser.Operation;

internal class Assignment : BaseOperation, IOperation
{
    public override int MaxStatements { get => 1; }
    public Assignment(IStatement value) : base("",[value])
    {
    }

}
