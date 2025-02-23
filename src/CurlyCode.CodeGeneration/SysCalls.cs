namespace CurlyCode.CodeGeneration;

public static class SysCalls
{
    public static void Exit(StreamWriter writer)
    {
        const int ExitSyscall = 60;
        writer.WriteLine($"mov rax, {ExitSyscall}");
        writer.WriteLine($"pop rdi");
        writer.WriteLine("syscall");
    }



}
