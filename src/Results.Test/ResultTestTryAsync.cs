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
}