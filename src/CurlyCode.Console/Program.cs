using CurlyCode.Console.Command;
using Spectre.Console.Cli;


var app = new CommandApp<CompileCommand>();

return app.Run(args);
