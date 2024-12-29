using CurlyCode.Parser.Statements;

namespace CurlyCode.Parser.Tests;

[TestClass]
public sealed class MathStatementTests
{
    public float Calculate(string math)
    {
        return new MathStatement().Calculate(math);
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
}
