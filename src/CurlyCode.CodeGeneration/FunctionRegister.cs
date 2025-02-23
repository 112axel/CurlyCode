using System.Runtime.CompilerServices;

namespace CurlyCode.CodeGeneration;

internal class FunctionRegister
{
    private Dictionary<string, Func<string>> Functions {get; set;}


    public void RegisterFunction(string name,Func<string> func)
    {
        if (Functions.ContainsKey(name))
        {
            throw new Exception($"Function {name} is already registered");
        }

        Functions.Add(name, func);
    }

    public string GetFunction(string name)
    {
        if (!Functions.ContainsKey(name))
        {
            throw new Exception($"Function {name} is not defined");
        }

        return Functions[name].Invoke();
    }
}
