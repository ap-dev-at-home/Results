using System.Diagnostics;

namespace Results.Test;

[TestClass]
public class ResultTestDoInterlocked
{
    [TestMethod]
    public void DoInterlockedReturnsResult()
    {
        var l = new object();
        var result = Result.DoInterlocked(() => Result.Ok(), l);
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public void DoInterlockedGenericReturnsResult()
    {
        var l = new object();
        var result = Result.DoInterlocked(() => Result.Ok(1), l);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    public void DoInterlockedDoesTakeLock()
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
    public void DoInterlockedDoesWaitTakenLock()
    {
        var l = new object();

        Stopwatch sw = new();
        
        var task0 = Task.Run(() =>
        {
            Result.DoInterlocked(() =>
            {
                Thread.Sleep(5);
                return Result.Ok();
            }, l, wait: true);
        });

        var task1 = Task.Run(() =>
        {
            sw.Start();
            var result = Result.DoInterlocked(() =>
            {
                sw.Stop();
                return Result.Ok();
            }, l, wait: true);
        });

        Task.WaitAll([task0, task1], 2500);

        Assert.IsFalse(sw.IsRunning);
        Assert.IsTrue(sw.ElapsedMilliseconds >= 5);
    }

    [TestMethod]
    public void DoInterlockedDoesNotWaitTakenLock()
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
            }, l, wait: false);
        });

        e1.WaitOne(2500);
        var result = Result.DoInterlocked(() =>
        {
            return Result.Ok();
        }, l, wait: false);

        e0.Set();

        Task.WaitAll([task0], 2500);

        Assert.IsTrue(result.Failed);
        Assert.IsInstanceOfType(result.Error, typeof(InterlockError));
    }

    [TestMethod()]
    public void DoInterlockedGenericDoesTakeLock()
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

    [TestMethod]
    public void DoInterlockedGenericDoesWaitTakenLock()
    {
        var l = new object();

        Stopwatch sw = new();

        var task0 = Task.Run(() =>
        {
            Result.DoInterlocked(() =>
            {
                Thread.Sleep(5);
                return Result.Ok(1);
            }, l, wait: true);
        });

        var task1 = Task.Run(() =>
        {
            sw.Start();
            var result = Result.DoInterlocked(() =>
            {
                sw.Stop();
                return Result.Ok(1);
            }, l, wait: true);
        });

        Task.WaitAll([task0, task1], 2500);

        Assert.IsFalse(sw.IsRunning);
        Assert.IsTrue(sw.ElapsedMilliseconds >= 5);
    }

    [TestMethod]
    public void DoInterlockedGenericDoesNotWaitTakenLock()
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
            }, l, wait: false).Value;
        });

        e1.WaitOne(2500);
        var result = Result.DoInterlocked(() =>
        {
            return Result.Ok(1);
        }, l, wait: false);

        e0.Set();

        Task.WaitAll([task0], 2500);

        Assert.AreEqual(1, value);
        Assert.IsTrue(result.Failed);
        Assert.IsInstanceOfType(result.Error, typeof(InterlockError));
    }

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
