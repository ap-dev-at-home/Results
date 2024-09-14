namespace Results.Test;

[TestClass]
public class ResultTests
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
        assert.Assert();
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
        assert.Assert();
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
        assert.Assert();
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
        assert.Assert();
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
        assert.Assert();
    }

    //--

    [TestMethod]
    public void ResultDoesNotNest()
    {
        string expected = "Value cannot be a Result object.";
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok(Result.Ok()), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok(Result.Ok(true)), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok(Result.Handover()), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok(Result.NotNull(new object())), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.NotNull(Result.Ok()), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok(Result.Fail()), expected);
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok<object?>().WhenNull(Result.Ok()), expected);
    }

    [TestMethod]
    public void ResultOkIsSuccessAndNotFailed()
    {
        var result = Result.Ok();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultFailIsFailedAndNotSuccess()
    {
        var result = Result.Fail();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultFailWithMessagesContainsMessages()
    {
        var result = Result.Fail("Error1", "Error2", "Error3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.AreEqual(3, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.IsInstanceOfType<Error>(result.Errors[2]);
        Assert.AreEqual("Error1", result.Errors[0].Message);
        Assert.AreEqual("Error2", result.Errors[1].Message);
        Assert.AreEqual("Error3", result.Errors[2].Message);
    }

    [TestMethod]
    public void ResultFailWithErrorsContainsErrors()
    {
        var result = Result.Fail(new Error("Error1"), new Error("Error2"), new Error("Error3"));
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.AreEqual(3, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.IsInstanceOfType<Error>(result.Errors[2]);
        Assert.AreEqual("Error1", result.Errors[0].Message);
        Assert.AreEqual("Error2", result.Errors[1].Message);
        Assert.AreEqual("Error3", result.Errors[2].Message);
    }

    [TestMethod]
    public void ResultOkGenericIsSuccessAndNotFailed()
    {
        var result = Result.Ok<bool>();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultOkGenericWithValueIsSuccessAndNotFailed()
    {
        var result = Result.Ok<bool>(true);
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(true, result.Value);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultFailGenericIsFailedAndNotSuccess()
    {
        var result = Result.Fail<bool>();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultFailGenericWithErrorsContainsMessages()
    {
        var result = Result.Fail<bool>("Error1", "Error2", "Error3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(3, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.IsInstanceOfType<Error>(result.Errors[2]);
        Assert.AreEqual("Error1", result.Errors[0].Message);
        Assert.AreEqual("Error2", result.Errors[1].Message);
        Assert.AreEqual("Error3", result.Errors[2].Message);
    }

    [TestMethod]
    public void ResultFailGenericWithErrorsContainsErrors()
    {
        var result = Result.Fail<bool>(new Error("Error1"), new Error("Error2"), new Error("Error3"));
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(3, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.IsInstanceOfType<Error>(result.Errors[2]);
        Assert.AreEqual("Error1", result.Errors[0].Message);
        Assert.AreEqual("Error2", result.Errors[1].Message);
        Assert.AreEqual("Error3", result.Errors[2].Message);
    }

    [TestMethod]
    public void ResultNotNullWithValueReturnsSuccess()
    {
        var value = new object();
        var result = Result.NotNull(value, "Error1", "Error2", "Error3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<object>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(value, result.Value);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void ResultNotNullWithNullReturnsFailed()
    {
        object? value = null;
        var result = Result.NotNull(value, "Error1", "Error2", "Error3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<object?>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(value, result.Value);
        Assert.AreEqual(3, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.IsInstanceOfType<Error>(result.Errors[2]);
        Assert.AreEqual("Error1", result.Errors[0].Message);
        Assert.AreEqual("Error2", result.Errors[1].Message);
        Assert.AreEqual("Error3", result.Errors[2].Message);
    }

    [TestMethod]
    public void HandoverResultIsCorrectlyFormedWithoutValues()
    {
        var handover = Result.Handover();
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(Handover));
        Assert.AreEqual(0, handover.Errors.Count);
        Assert.AreEqual(true, handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(0, handover.Value.Length);
    }

    [TestMethod]
    public void HandoverResultIsCorrectlyFormedWithValues()
    {
        var handover = Result.Handover(1, 2, 3);
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(Handover));
        Assert.AreEqual(0, handover.Errors.Count);
        Assert.AreEqual(true, handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(3, handover.Value.Length);
        Assert.AreEqual(1, handover.Value[0]);
        Assert.AreEqual(2, handover.Value[1]);
        Assert.AreEqual(3, handover.Value[2]);
    }

    [TestMethod]
    public void HandoverResultAcceptsResult()
    {
        var handover = Result.Handover(Result.Ok(), null, Result.Fail());
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(Handover));
        Assert.AreEqual(0, handover.Errors.Count);
        Assert.AreEqual(true, handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(3, handover.Value.Length);
        Assert.IsInstanceOfType<Result>(handover.Value[0]);
        Assert.AreEqual(true, ((Result)handover.Value[0]).Success);
        Assert.IsNull(handover.Value[1]);
        Assert.IsInstanceOfType<Result>(handover.Value[2]);
        Assert.AreEqual(false, ((Result)handover.Value[2]).Success);
    }

    [TestMethod]
    public void HandoverResultAcceptsNonResult()
    {
        var handover = Result.Handover(new object(), new object());
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(Handover));
        Assert.AreEqual(0, handover.Errors.Count);
        Assert.AreEqual(true, handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(2, handover.Value.Length);
        Assert.IsInstanceOfType<object>(handover.Value[0]);
        Assert.IsInstanceOfType<object>(handover.Value[1]);
    }

    [TestMethod]
    public void FailFastWithNoCallsReturnsSuccess()
    {
        var result = Result.FailFast();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void FailFastWithNoFailedCallsReturnsSuccess()
    {
        var i = 0;

        var result = Result.FailFast([
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Ok(); }
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
        Assert.AreEqual(3, i);
    }

    [TestMethod]
    public void FailFastWithFailedCallsReturnsFail()
    {
        var i = 0;

        var result = Result.FailFast([
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Fail("Error2"); },
            () => { i++; return Result.Ok(); }
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(1, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.AreEqual("Error2", result.Errors[0].Message);
        Assert.AreEqual(2, i);
    }

    [TestMethod]
    public void FailSafeWithNoCallsReturnsSuccess()
    {
        var result = Result.FailSafe();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
    }

    [TestMethod]
    public void FailSafeWithNoFailedCallsExecutesAllAndReturnsSuccess()
    {
        var i = 0;

        var result = Result.FailSafe([
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Ok(); }
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(0, result.Errors.Count);
        Assert.AreEqual(3, i);
    }

    [TestMethod]
    public void FailSafeWithFailedCallsExecutesAllAndReturnsFail()
    {
        var i = 0;

        var result = Result.FailSafe([
            () => { i++; return Result.Ok(); },
            () => { i++; return Result.Fail("Error2"); },
            () => { i++; return Result.Fail("Error3"); },
            () => { i++; return Result.Ok(); }
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(2, result.Errors.Count);
        Assert.IsInstanceOfType<Error>(result.Errors[0]);
        Assert.AreEqual("Error2", result.Errors[0].Message);
        Assert.IsInstanceOfType<Error>(result.Errors[1]);
        Assert.AreEqual("Error3", result.Errors[1].Message);
        Assert.AreEqual(4, i);
    }
}