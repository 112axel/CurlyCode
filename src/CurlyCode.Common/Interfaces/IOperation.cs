namespace CurlyCode.Common.Interfaces;

public interface IOperation
{
    List<IStatement> Statements { get; set; }
    int MaxStatements { get; }
    string Identifier { get; }
}
