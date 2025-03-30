namespace CurlyCode.Common.Classes;

public class SystemFunction
{
    public int AmountOfStatements = 0;
    public string Name = "";
    public SystemFunction(string name, int amountOfStatements)
    {
        AmountOfStatements = amountOfStatements;
        Name = name;
    }


    public static List<SystemFunction> systemFunctions = new List<SystemFunction>()
    {
        new SystemFunction("exit",1),
    };
}
