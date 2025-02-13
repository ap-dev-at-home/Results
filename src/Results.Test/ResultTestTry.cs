using System.Collections.ObjectModel;

namespace Results.Test;

[TestClass]
public class ResultTestsTry
{
    [TestMethod]
    public void ResultTryExecutesAction()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((Action)(() =>
        {
            assertTry.Assert(() => true);
        }), (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.AreEqual(true, result.Success);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryActionExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((Action)(() =>
        {
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
        }), (Exception ex) =>
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
    public void ResultTryExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
            return Result.Ok();
        }, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
        }, (Exception ex) =>
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
    public void ResultTryGenericExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
            return Result.Ok(1);
        }, (Exception ex) => 
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Value);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryGenericExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try<int>(() =>
        {
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
        }, (Exception ex) =>
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
}