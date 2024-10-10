using System.Diagnostics;

namespace Results.Test;

[TestClass]
public class ResultTestTryInterlocked
{
    [TestMethod]
    public void TryInterlockedReturnsResult()
    {
        var l = new object();
        var result = Result.TryInterlocked(() => Result.Ok(), l);
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public void TryInterlockedGenericReturnsResult()
    {
        var l = new object();
        var result = Result.TryInterlocked(() => Result.Ok(1), l);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Value);
    }
}
