using System.Collections.ObjectModel;

namespace Results.Test;

[TestClass]
public class ResultTestsTryAsync
{
    [TestMethod]
    public async Task ResultTryAsyncExecutesFuncAsync()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async () =>
        {
            await Task.CompletedTask;
            assertTry.Assert(() => true);
            return Result.Ok(1);
        }, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.AreEqual(true, result.Success);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public async Task ResultTryAsyncFuncExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async () =>
        {
            await Task.CompletedTask;
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
        }, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
            Assert.AreEqual("Exception was thrown", ex.Message);
        });

        Assert.AreEqual(false, result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<ExceptionError>(result.Error);
        Assert.AreEqual("Exception was thrown", ((ExceptionError)result.Error).Exception.Message);
        assertTry.Assert(true, 1);
        assertCatch.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultTryAsyncT1ExecutesFuncAsync()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i) =>
        {
            await Task.CompletedTask;
            assertTry.Assert(() => i == 1);
            return Result.Ok(1);
        }, 1, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public async Task ResultTryAsyncFuncT1ExecutesAsync()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i) =>
        {
            await Task.CompletedTask;
            assertTry.Assert(() => i == 1);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
        }, 1, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
            Assert.AreEqual("Exception was thrown", ex.Message);
        });

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<ExceptionError>(result.Error);
        Assert.AreEqual("Exception was thrown", ((ExceptionError)result.Error).Exception.Message);
        assertTry.Assert(true, 1);
        assertCatch.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultTryAsyncT2ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            await Task.CompletedTask;
            return Result.Ok(1);
        }, 1, 2, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 2);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public async Task ResultTryAsyncFuncT2ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
        }, 1, 2, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
            Assert.AreEqual("Exception was thrown", ex.Message);
        });

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<ExceptionError>(result.Error);
        Assert.AreEqual("Exception was thrown", ((ExceptionError)result.Error).Exception.Message);
        assertTry.Assert(true, 2);
        assertCatch.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultTryAsyncT3ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            await Task.CompletedTask;
            return Result.Ok(1);
        }, 1, 2, 3, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 3);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public async Task ResultTryAsyncFuncT3ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
        }, 1, 2, 3, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
            Assert.AreEqual("Exception was thrown", ex.Message);
        });

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<ExceptionError>(result.Error);
        Assert.AreEqual("Exception was thrown", ((ExceptionError)result.Error).Exception.Message);
        assertTry.Assert(true, 3);
        assertCatch.Assert(true, 1);
    }

    [TestMethod]
    public async Task ResultTryAsyncT4ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
            await Task.CompletedTask;
            return Result.Ok(1);
        }, 1, 2, 3, 4, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.AreEqual(true, result.Success);
        assertTry.Assert(true, 4);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public async Task ResultTryAsyncFuncT4ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = await Result.TryAsync(async (i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
        }, 1, 2, 3, 4, async (Exception ex) =>
        {
            assertCatch.Assert(() => true);
            Assert.AreEqual("Exception was thrown", ex.Message);
        });

        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<ExceptionError>(result.Error);
        Assert.AreEqual("Exception was thrown", ((ExceptionError)result.Error).Exception.Message);
        assertTry.Assert(true, 4);
        assertCatch.Assert(true, 1);
    }
}