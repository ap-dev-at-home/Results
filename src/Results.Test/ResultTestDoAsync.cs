using System.Collections.ObjectModel;

namespace Results.Test;

[TestClass]
public class ResultTestsDoAsync
{
    [TestMethod]
    public async Task ResultDoAsyncExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = await Result.DoAsync(async () =>
        {
            await Task.CompletedTask;
            assert.Assert(() => true);
            return Result.Ok(1);
        });

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1, result.Value);
        assert.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultDoAsyncT1ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = await Result.DoAsync(async (i) =>
        {
            await Task.CompletedTask;
            assert.Assert(() => i == 1);
            return Result.Ok(i);
        }, 1);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1, result.Value);
        assert.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultDoAsyncT2ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = await Result.DoAsync(async (i, j) =>
        {
            await Task.CompletedTask;
            assert.Assert(() => i == 1);
            assert.Assert(() => j == 2);
            return Result.Ok(i + j);
        }, 1, 2);

        Assert.AreEqual(true, result.Success);
        Assert.AreEqual(1 + 2, result.Value);
        assert.Assert(true, 2);
    }

    [TestMethod]
    public async Task ResultDoAsyncT3ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = await Result.DoAsync(async (i, j, k) =>
        {
            await Task.CompletedTask;
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
    public async Task ResultDoAsyncT4ExecutesFunc()
    {
        var assert = new AssertFlagPassthrough();

        var result = await Result.DoAsync(async (i, j, k, l) =>
        {
            await Task.CompletedTask;
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