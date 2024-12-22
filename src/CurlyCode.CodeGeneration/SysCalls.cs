namespace CurlyCode.CodeGeneration;

public static class SysCalls
{
    public static void Exit(StreamWriter writer,int exitCode = 0)
    {
        const int ExitSyscall = 60;
        writer.WriteLine($"mov rax, {ExitSyscall}");
        writer.WriteLine($"mov rdi, {exitCode}");
        writer.WriteLine("syscall");
    }


}
