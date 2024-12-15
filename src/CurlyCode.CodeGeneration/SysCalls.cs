namespace CurlyCode.CodeGeneration;

public static class SysCalls
{
    public static void Exit(StreamWriter writer)
    {

        writer.WriteLine("mov rax, 60");
        writer.WriteLine("mov rdi, 20");
        writer.WriteLine("syscall");
    }


}
