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
}