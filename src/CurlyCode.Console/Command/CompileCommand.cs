using Spectre.Console.Cli;
using CurlyCode.Lexer;
using CurlyCode.Common.FileService;
using Spectre.Console;
namespace CurlyCode.Console.Command;

internal sealed class CompileCommand : Command<CompileCommand.Settings>
{
    public override int Execute(CommandContext context, CompileCommand.Settings settings)
    {
        var tokens = Lexer.Lexer.Run(FileService.GetStream(settings.Path));

        AnsiConsole.Write(0);
        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0,"[Path]")]
        public string? Path { get; init; }
    }
}
