using System.Security;

namespace CurlyCode.Parser.Statements;

public class MathStatement 
{
    public MathStatement()
    {

    }

    enum Operation
    {
       Add,
       Sub,
       Divide,
       Mult,
       TermNumber
    }

    public float Calculate(string mathInput)
    {
        var a = GetExecutionTree(new() { Operation.Add,Operation.Mult,Operation.Mult});
        return 0;
    }

    private TreeMath GetExecutionTree(List<Operation> text)
    {
        var number = 0;
        var list2 = new List<TreeMath>();
        list2.Add(new TreeMath(number++));
        foreach (Operation op in text)
        {
            list2.Add(new TreeMath(op));
            list2.Add(new TreeMath(number++));
        }

        for (int i = 0; i < list2.Count; i++)
        {
            TreeMath operation = list2[i];
            if(operation.Operation == Operation.Mult)
            {
                var term1 = list2[i - 1];
                var term2 = list2[i + 1];
                list2[i] = new TreeMath(Operation.Mult, term1, term2);
                list2.Remove(term1);
                list2.Remove(term2);
                i -= 1;
            }
        }
        return list2.First();

    }

    private bool IsNumber(string text)
    {
        return int.TryParse(text, out var number);
    }

    private class TreeMath
    {
        public TreeMath(int termNumber)
        {
            Operation = Operation.TermNumber;
            TermNumber = termNumber;
        }

        public TreeMath(Operation operation, TreeMath term1 ,TreeMath term2)
        {
            Operation = operation;
            Term1 = term1;
            Term2 = term2;
        }

        public TreeMath(Operation operation)
        {
            Operation = operation;
        }

        public Operation Operation { get; set; }
        public TreeMath? Term1 { get; set; }
        public TreeMath? Term2 { get; set; }
        public int? TermNumber { get; set; }
    }
}
