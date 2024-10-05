namespace Results.Test;

[TestClass]
public class ResultTestInterlocked
{
    [TestMethod]
    public void DoInterlockedReturnsResult()
    {
        var l = new object();
        var result = Result.DoInterlocked(() => Result.Ok(), l);
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public void DoInterlockedDoesWaitLock()
    {
        var l = new object();
        
        ManualResetEvent e0 = new(initialState: false);
        ManualResetEvent e1 = new(initialState: false);

        var task0 = Task.Run(() =>
        {
            Result.DoInterlocked(() =>
            {
                e1.Set();
                e0.WaitOne(2500);
                return Result.Ok();
            }, l, wait: true);
        });

        e1.WaitOne(2500);
        bool canEnter = Monitor.TryEnter(l);

        e0.Set();
        
        Task.WaitAll([task0], 2500);

        Assert.IsFalse(canEnter);
    }

    [TestMethod]
    public void DoInterlockedGenericReturnsResult()
    {
        var l = new object();
        var result = Result.DoInterlocked(() => Result.Ok(1), l);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod()]
    public void DoInterlockedGenericDoesWaitLock()
    {
        var l = new object();

        ManualResetEvent e0 = new(initialState: false);
        ManualResetEvent e1 = new(initialState: false);

        int value = 0;
        var task0 = Task.Run(() =>
        {
            value = Result.DoInterlocked(() =>
            {
                e1.Set();
                e0.WaitOne(2500);
                return Result.Ok(1);
            }, l, wait: true).Value;
        });

        e1.WaitOne(2500);
        bool canEnter = Monitor.TryEnter(l);

        e0.Set();

        Task.WaitAll([task0], 2500);

        Assert.IsFalse(canEnter);
        Assert.AreEqual(1, value);
    }
}
