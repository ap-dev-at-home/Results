using System.Collections.ObjectModel;

namespace Results.Test;

[TestClass]
public class ResultTestsDo
{
    [TestMethod]
    public void ResultDoExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do(() => 
        { 
            assert.Assert(() => true);
            return Result.Ok(1);
        });
        
        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1, result.Value);
        assert.Assert(true, 1);
    }

    [TestMethod]
    public void ResultDoT1ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do((i) =>
        {
            assert.Assert(() => i == 1);
            return Result.Ok(i);
        }, 1);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1, result.Value);
        assert.Assert(true, 1);
    }

    [TestMethod]
    public void ResultDoT2ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do((i, j) =>
        {
            assert.Assert(() => i == 1);
            assert.Assert(() => j == 2);
            return Result.Ok(i + j);
        }, 1, 2);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1 + 2, result.Value);
        assert.Assert(true, 2);
    }

    [TestMethod]
    public void ResultDoT3ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do((i, j, k) =>
        {
            assert.Assert(() => i == 1);
            assert.Assert(() => j == 2);
            assert.Assert(() => k == 3);
            return Result.Ok(i + j + k);
        }, 1, 2, 3);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1 + 2 + 3, result.Value);
        assert.Assert(true, 3);
    }

    [TestMethod]
    public void ResultDoT4ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = Result.Do((i, j, k, l) =>
        {
            assert.Assert(() => i == 1);
            assert.Assert(() => j == 2);
            assert.Assert(() => k == 3);
            assert.Assert(() => l == 4);
            return Result.Ok(i + j + k + l);
        }, 1, 2, 3, 4);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1 + 2 + 3 + 4, result.Value);
        assert.Assert(true, 4);
    }
}