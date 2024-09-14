﻿namespace Results.Test;

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
    public void DoThenCallHandoverValues()
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
}