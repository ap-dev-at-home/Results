using System.Diagnostics;

namespace Results.Test;

[TestClass]
public class ResultTestTryInterlocked
{
    private const int SLEEP = 5;

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

    [TestMethod]
    public void TryInterlockedDoesTakeLock()
    {
        var l = new object();

        var e0 = new ManualResetEvent(initialState: false);
        var e1 = new ManualResetEvent(initialState: false);

        var task0 = Task.Run(() =>
        {
            Result.TryInterlocked(() =>
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
    public void TryInterlockedDoesWaitTakenLock()
    {
        var l = new object();

        var e0 = new ManualResetEvent(initialState: false);
        
        var sw = new Stopwatch();
        
        var task0 = Task.Run(() =>
        {
            Result.TryInterlocked(() =>
            {
                e0.Set();
                Thread.Sleep(SLEEP);
                return Result.Ok();
            }, l, wait: true);
        });

        e0.WaitOne(2500);

        var task1 = Task.Run(() =>
        {
            sw.Start();
            Result.TryInterlocked(() =>
            {
                sw.Stop();
                return Result.Ok();
            }, l, wait: true);
        });

        Task.WaitAll([task0, task1], 2500);

        Assert.IsFalse(sw.IsRunning);
        Assert.IsTrue(sw.ElapsedMilliseconds >= SLEEP);
    }
}
