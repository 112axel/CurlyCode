using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

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
        var ops = GetOperations(mathInput);
        var numbers = GetNumbers(mathInput);
        var calculationPlan = GetExecutionTree(ops);

        
        return ExecutePlan(calculationPlan,numbers);
    }

    private Dictionary<int,int> GetNumbers(string mathInput)
    {
        var output = new Dictionary<int, int>();
        Regex findNumbers = new Regex(@"(?:[/*+-]|^)-?(\d)");

        var a = findNumbers.Matches(mathInput);
        for(int i = 0; i<a.Count; i++)
        {
            output[i] = int.Parse(a[i].Groups[1].ToString());
        }
        return output;
    }

    private float ExecutePlan(TreeMath treeNode,Dictionary<int,int> values)
    {
        if(treeNode.Operation == Operation.TermNumber)
        {
            if (treeNode.TermNumber == null)
            {
                throw new Exception("Should always have a term number");
            }
            //?? is needed for some reason
            return values[treeNode.TermNumber??0];
        }
        if (treeNode.Operation == Operation.Add)
            return ExecutePlan(treeNode.Term1!,values) + ExecutePlan(treeNode.Term2!,values);
        else if (treeNode.Operation == Operation.Sub)
            return ExecutePlan(treeNode.Term1!,values) - ExecutePlan(treeNode.Term2!,values);
        else if (treeNode.Operation == Operation.Mult)
            return ExecutePlan(treeNode.Term1!,values) * ExecutePlan(treeNode.Term2!,values);
        else if (treeNode.Operation == Operation.Divide)
            return ExecutePlan(treeNode.Term1!,values) / ExecutePlan(treeNode.Term2!,values);
        else
            throw new Exception("Invalid operation");
    }

    private List<Operation> GetOperations(string mathInput)
    {
        mathInput = mathInput.Replace(" ", "");
        var builder = new StringBuilder();
        var doubleSign = false;
        foreach (var a in mathInput)
        {
            if (char.IsDigit(a))
            {
                doubleSign = false;
                continue;
            }
            if (doubleSign)
            {
                continue;
            }
            doubleSign = true;
            builder.Append(a);
        }
        var alaa = builder.ToString();

        var output = new List<Operation>();
        foreach (var a in alaa)
        {
            var d = a switch
            {
                '+' => Operation.Add,
                '-' => Operation.Sub,
                '*' => Operation.Mult,
                '/' => Operation.Divide,
                _ => throw new NotImplementedException()
            };
            output.Add(d);
        }

        return output;
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

        Reduce(list2);
        if (list2.Count > 1)
        {
            throw new Exception("multiple nodes left");
        }

        return list2.First();

    }

    private void Reduce(List<TreeMath> treeMaths)
    {
        ReduceTreeMath(treeMaths, [Operation.Mult, Operation.Divide]);
        ReduceTreeMath(treeMaths, [Operation.Add, Operation.Sub]);
    }

    private void ReduceTreeMath(List<TreeMath> list, List<Operation> operations)
    {
        for (int i = 0; i < list.Count; i++)
        {
            TreeMath operation = list[i];
            if (operations.Contains(operation.Operation))
            {
                var term1 = list[i - 1];
                var term2 = list[i + 1];
                list[i] = new TreeMath(operation.Operation, term1, term2);
                list.Remove(term1);
                list.Remove(term2);
                i -= 1;
            }
        }
    }

    private class TreeMath
    {
        public TreeMath(int termNumber)
        {
            Operation = Operation.TermNumber;
            TermNumber = termNumber;
        }

        public TreeMath(Operation operation, TreeMath term1, TreeMath term2)
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
