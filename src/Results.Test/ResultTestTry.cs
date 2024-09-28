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

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
        }, (Exception ex) =>
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

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
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
    public void ResultTryT1ExecutesAction()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i) =>
        {
            assertTry.Assert(() => i == 1);
        }, 1, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryActionT1ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i) =>
        {
            assertTry.Assert(() => i == 1);
            throw new Exception("Exception was thrown");
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
    public void ResultTryT2ExecutesAction()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
        }, 1, 2, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 2);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryActionT2ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            throw new Exception("Exception was thrown");
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
    public void ResultTryT3ExecutesAction()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
        }, 1, 2, 3, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        assertTry.Assert(true, 3);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryActionT3ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            throw new Exception("Exception was thrown");
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
    public void ResultTryT4ExecutesAction()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
        }, 1, 2, 3, 4, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.AreEqual(true, result.Success);
        assertTry.Assert(true, 4);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryActionT4ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
            throw new Exception("Exception was thrown");
        }, 1, 2, 3, 4, (Exception ex) =>
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

    [TestMethod]
    public void ResultTryExecutesFunc()
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
    public void ResultTryExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try(() =>
        {
            assertTry.Assert(() => true);
            throw new Exception("Exception was thrown");
            return Result.Ok(1);
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
    public void ResultTryT1ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i) =>
        {
            assertTry.Assert(() => i == 1);
            return Result.Ok(i);
        }, 1, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, result.Value);
        assertTry.Assert(true, 1);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryT1ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i) =>
        {
            assertTry.Assert(() => i == 1);
            throw new Exception("Exception was thrown");
            return Result.Ok(i);
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
    public void ResultTryT2ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            return Result.Ok(i + j);
        }, 1, 2, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2, result.Value);
        assertTry.Assert(true, 2);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryT2ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            throw new Exception("Exception was thrown");
            return Result.Ok(i + j);
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
    public void ResultTryT3ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            return Result.Ok(i + j + k);
        }, 1, 2, 3, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2 + 3, result.Value);
        assertTry.Assert(true, 3);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryT3ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            throw new Exception("Exception was thrown");
            return Result.Ok(i + j + k);
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
    public void ResultTryT4ExecutesFunc()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
            return Result.Ok(i + j + k + l);
        }, 1, 2, 3, 4, (Exception ex) =>
        {
            assertCatch.Assert(() => true);
        });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1 + 2 + 3 + 4, result.Value);
        assertTry.Assert(true, 4);
        assertCatch.Assert(false, 0);
    }

    [TestMethod]
    public void ResultTryT4ExecutesCatch()
    {
        var assertTry = new AssertFlagPassthrough();
        var assertCatch = new AssertFlagPassthrough();

        var result = Result.Try((i, j, k, l) =>
        {
            assertTry.Assert(() => i == 1);
            assertTry.Assert(() => j == 2);
            assertTry.Assert(() => k == 3);
            assertTry.Assert(() => l == 4);
            throw new Exception("Exception was thrown");
            return Result.Ok(i + j + k + l);
        }, 1, 2, 3, 4, (Exception ex) =>
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