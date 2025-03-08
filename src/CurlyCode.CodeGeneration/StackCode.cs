using CurlyCode.Common.Enums;

namespace CurlyCode.CodeGeneration;

public static class StackCode
{
    public static void AddValueToStack(StreamWriter writer, int number)
    {
        writer.WriteLine($"push {number}");
    }

    public static void GetValueFromStack(StreamWriter writer,int offset)
    {
        writer.WriteLine($"mov rax,[rbp-{offset*8}]");
    }

    public static void PutOnTopOfStack(this StreamWriter writer, int offset)
    {
        GetValueFromStack(writer, offset);
        writer.WriteLine($"push rax");
    }

    //public static void PopValue(StreamWriter writer) {
    //    writer.WriteLine("");
    //}
}
