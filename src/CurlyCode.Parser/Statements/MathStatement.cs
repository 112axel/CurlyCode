using System.ComponentModel.Design;
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
       TermNumber,
       OpenParenthesis,
       ClosedParenthesis
    }

    public float Calculate(string mathInput)
    {
        var ops = GetOperations(mathInput);
        var numbers = GetNumbers(mathInput);
        var calculationPlan = GetExecutionTree(new(ops));

        var maps = MapNumbers(numbers,calculationPlan);
        
        return ExecutePlan(calculationPlan,maps);
    }

    private Dictionary<TreeMath, int> MapNumbers(Dictionary<int,int> numbers,TreeMath plan)
    {
        var a = PostOrderSearch(plan);
        Dictionary<TreeMath, int> output = [];
        int counter = 0;
        foreach (var node in a)
        {
            if(node.Operation == Operation.TermNumber)
            {
                output.Add(node, numbers[counter]);
                counter++;
            }
        }
        return output;
    }

    private List<TreeMath> PostOrderSearch(TreeMath treeMath, List<TreeMath>? output = null)
    {
        if (output == null)
        {
            output = new List<TreeMath>();
        }

        output.Add(treeMath);

        if (treeMath.Operation == Operation.TermNumber)
        {
            return output;
        }

        if(treeMath.Term1 != null)
        {
            PostOrderSearch(treeMath.Term1, output);
        }
        if(treeMath.Term2 != null)
        {
            PostOrderSearch(treeMath.Term2, output);
        }
        return output;
    }

    private Dictionary<int,int> GetNumbers(string mathInput)
    {
        var output = new Dictionary<int, int>();
        Regex findNumbers = new Regex(@"(?:[/*+\-(]|^)-?(\d)");

        var a = findNumbers.Matches(mathInput);
        for(int i = 0; i<a.Count; i++)
        {
            output[i] = int.Parse(a[i].Groups[1].ToString());
        }
        return output;
    }

    private float ExecutePlan(TreeMath treeNode,Dictionary<TreeMath,int> values)
    {
        if(treeNode.Operation == Operation.TermNumber)
        {
            return values[treeNode];
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
            if(a == '(' || a == ')')
            {
                builder.Append(a);
                continue;
            }
            if (char.IsDigit(a) )
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
                '(' => Operation.OpenParenthesis,
                ')' => Operation.ClosedParenthesis,
                _ => throw new NotImplementedException()
            };
            output.Add(d);
        }

        return output;
    }

    private TreeMath GetExecutionTree(Queue<Operation> stack)
    {
        var a = GetTreeMaths(stack);

        var output = Reduce(a);
        if (output.Count > 1)
        {
            throw new Exception("multiple nodes left");
        }

        return output.First();

    }

    private List<TreeMath> GetTreeMaths(Queue<Operation> stack)
    {
        var list = new List<TreeMath>();
        bool first = true;
        while (stack.Count > 0)
        {
            var value = stack.Dequeue();
            if(value == Operation.OpenParenthesis || value == Operation.ClosedParenthesis)
            {
                list.Add(new(value));
            }
            else
            {
                if (list.LastOrDefault()?.Operation != Operation.ClosedParenthesis)
                {
                    list.Add(new());
                    //first = false;
                }
                list.Add(new(value));
                if(stack.Count > 0 && stack.Peek() == Operation.ClosedParenthesis)
                {
                    list.Add(new());
                }
            }
        }
        list.Add(new());

        return list;
    }

    private List<TreeMath> Reduce(List<TreeMath> treeMaths, int index = 0)
    {
        var a = ReduceTreeMath(treeMaths, [Operation.Mult, Operation.Divide], index);
        var list = ReduceTreeMath(a, [Operation.Add, Operation.Sub], index);
        if (index != 0)
        {
            list.RemoveAt(index + 1);
            list.RemoveAt(index - 1);
        }
        return list;
    }

    private List<TreeMath> ReduceTreeMath(List<TreeMath> list, List<Operation> operations, int index)
    {
        for (int i = index; i < list.Count; i++)
        {
            TreeMath operation = list[i];
            if (operation.Operation == Operation.ClosedParenthesis)
            {
                //list.Remove(operation);
                return list;
            }

            if (operation.Operation == Operation.OpenParenthesis)
            {
                Reduce(list, i + 1);
            }

            if (operations.Contains(operation.Operation))
            {
                var term1 = list[i - 1];
                if (list[i + 1].Operation == Operation.OpenParenthesis)
                {
                    Reduce(list, i + 2);
                }
                var term2 = list[i + 1];
                list[i] = new TreeMath(operation.Operation, term1, term2);
                list.Remove(term1);
                list.Remove(term2);
                i -= 1;
            }
        }
        return list;
    }

    private class TreeMath
    {
        public TreeMath()
        {
            Operation = Operation.TermNumber;
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
    }
}
