using static CurlyCode.Parser.Statements.MathStatement;

namespace CurlyCode.CodeGeneration;

public static class MathCode
{
    public static void Add(this StreamWriter writer)
    {
        writer.PopRax();
        writer.PopRbx();
        writer.WriteLine("add rax,rbx");
        writer.PushRax();
    }

    public static void Subtract(this StreamWriter writer)
    {
        writer.PopRax();
        writer.PopRbx();
        writer.WriteLine("sub rax,rbx");
        writer.PushRax();
    }

    public static void Multiply(this StreamWriter writer)
    {
        writer.PopRax();
        writer.PopRbx();
        writer.WriteLine("imul rax,rbx");
        writer.PushRax();
    }


}
