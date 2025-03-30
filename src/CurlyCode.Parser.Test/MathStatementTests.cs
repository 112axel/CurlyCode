using CurlyCode.Parser.Statements;

namespace CurlyCode.Parser.Tests;

[TestClass]
public sealed class MathStatementTests
{
    public float Calculate(string math)
    {
        return new MathStatement(new()).Calculate(math);
    }

    [TestMethod]
    public void BasicAdd()
    {
        var math = "5+6";
        Assert.AreEqual(Calculate(math), 11);
    }

    [TestMethod]
    public void BasicMultiplication()
    {
        var math = "6*2";
        Assert.AreEqual(Calculate(math), 12);
    }

    [TestMethod]
    public void PrecedenceMult()
    {
        var math = "5+6*2";
        Assert.AreEqual(Calculate(math), 17);
    }

    [TestMethod]
    public void ParenthesisPrecedence()
    {
        var math = "(5+6)*2";
        Assert.AreEqual(Calculate(math), 22);
    }

    [TestMethod]
    public void ComplexMath()
    {
        var math = "(5*(6+2))*2";
        Assert.AreEqual(Calculate(math), 80);
    }

    [TestMethod]
    public void Advanced()
    {
        var math = "5+2*5*4";
        Assert.AreEqual(Calculate(math), 45);
    }

    [TestMethod]
    public void PostFixTest()
    {
        var tree = new MathStatement.TreeMath()
        {
            Term1 = new MathStatement.TreeMath()
            {
                Term1 = new MathStatement.TreeMath()
                {
                    Operation = MathStatement.Operation.TermNumber,
                },
                Term2 = new MathStatement.TreeMath()
                {
                    Operation = MathStatement.Operation.TermNumber,
                },
                Operation = MathStatement.Operation.Add
            },
            Term2 = new MathStatement.TreeMath()
            {
                Term1 = new MathStatement.TreeMath()
                {
                    Operation = MathStatement.Operation.TermNumber,
                },
                Term2 = new MathStatement.TreeMath()
                {
                    Operation = MathStatement.Operation.TermNumber,
                },
                Operation = MathStatement.Operation.Add
            },
            Operation = MathStatement.Operation.Mult,

        };
        var a = new MathStatement(new()).ReversePolishNotation(tree,null);

        var expectedOperations = new List<MathStatement.Operation>() {
            MathStatement.Operation.TermNumber,
            MathStatement.Operation.TermNumber,
            MathStatement.Operation.Add,
            MathStatement.Operation.TermNumber,
            MathStatement.Operation.TermNumber,
            MathStatement.Operation.Add,
            MathStatement.Operation.Mult,
        };
        Assert.IsTrue(CompareList(a,expectedOperations));

    }

    private bool CompareList(List<MathStatement.TreeMath> treeMaths, List<MathStatement.Operation> operations)
    {
        for (int i = 0; i < treeMaths.Count; i++)
        {
            if (treeMaths[i].Operation != operations[i])
            {
                return false;
            }
        }
        return true;
    }
}
