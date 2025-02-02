using CurlyCode.CodeGeneration;

namespace CurlyCode.Parser;

public static class StackAbstraction
{

    static StackAbstraction()
    {

    }

    static Dictionary<string, int> VariablePosition;
    static int TopOfStack;

    public static int GetAddress(string variableName)
    {
        return VariablePosition[variableName];
    }

    public static void AddVariable(string variableName)
    {
        VariablePosition[variableName] = TopOfStack;
        TopOfStack++;
       // StackCode.
    }
}
