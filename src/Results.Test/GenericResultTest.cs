namespace Results.Test;

[TestClass]
public class GenericResultTest
{
    [TestMethod]
    public void WhenNullSetsNoValueWhenFailed()
    {
        var result = Result.Fail<object?>();
        Assert.IsNull(result.Value);
        var o = new object();
        result.WhenNull(o);
        Assert.AreEqual(null, result.Value);
    }

    [TestMethod]
    public void WhenNullSetsValueWhenNull()
    {
        var result = Result.Ok<object?>(null);
        Assert.IsNull(result.Value);
        var o = new object();
        result.WhenNull(o);
        Assert.AreEqual(o, result.Value);
    }

    [TestMethod]
    public void WhenNullReturnsItself()
    {
        var result0 = Result.Ok<object?>(null);
        var result1 = result0.WhenNull(new object());
        Assert.AreEqual(result0, result1);
    }

    [TestMethod]
    public void ResultOkThenFailReturnsFailedResult()
    {
        var result = Result.Ok(1).Then(i => Result.Fail<int>());
        Assert.IsInstanceOfType(result, typeof(Result<int>));
        Assert.IsFalse(result.Success);
        Assert.AreEqual(0, result.Value);
    }

    [TestMethod]
    public void ResultFailThenNotCalled()
    {
        var assert = new AssertFlagPassthrough();
        var result = Result.Fail<int>().Then(i => { assert.Assert(() => true); return Result.Ok(i + 1); });
        Assert.IsInstanceOfType(result, typeof(Result<int>));
        Assert.AreEqual(result.Value, 0);
        Assert.IsFalse(result.Success);
        assert.Assert(false, 0);
    }

    [TestMethod]
    public void ResultOkThenOkReturnsOkResult()
    {
        var result = Result.Ok(1).Then(i => Result.Ok<int>(i + 1));
        Assert.IsInstanceOfType(result, typeof(Result<int>));
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Value);
    }

    [TestMethod]
    public void DoThenCallHandover1Values()
    {
        var result = Result.Do(() =>
        {
            return Result.Ok(1);
        }).Then(i => Result.Ok(i + 1));

        Assert.IsInstanceOfType(result, typeof(Result<int>));
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Value);
    }

    [TestMethod]
    public void DoThenCallHandover2Values()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do(() =>
        {
            return Result.Handover(1, 2);
        }).Then((int a, int b) =>
        {
            assert.Assert(() => a == 1);
            assert.Assert(() => b == 2);

            return Result.Ok(1 + 2);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2, result.Value);
        assert.Assert(true, 2);
    }

    [TestMethod]
    public void DoThenCallHandover3Values()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do(() =>
        {
            return Result.Handover(1, 2, 3);
        }).Then((int a, int b, int c) => 
        {
            assert.Assert(() => a == 1);
            assert.Assert(() => b == 2);
            assert.Assert(() => c == 3);

            return Result.Ok(1 + 2 + 3);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2 + 3, result.Value);
        assert.Assert(true, 3);
    }

    [TestMethod]
    public void DoThenCallHandover4Values()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do(() =>
        {
            return Result.Handover(1, 2, 3, 4);
        }).Then((int a, int b, int c, int d) =>
        {
            assert.Assert(() => a == 1);
            assert.Assert(() => b == 2);
            assert.Assert(() => c == 3);
            assert.Assert(() => d == 4);

            return Result.Ok(1 + 2 + 3 + 4);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2 + 3 + 4, result.Value);
        assert.Assert(true, 4);
    }
}
