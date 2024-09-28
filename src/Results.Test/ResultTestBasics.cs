using System.Collections.ObjectModel;

namespace Results.Test;

[TestClass]
public class ResultTestsBasics
{
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
        Assert.ThrowsException<InvalidOperationException>(() => Result.Ok<object?>().When(v => true, v => Result.Ok()), expected);
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
        Assert.IsNull(result.Error);
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
        Assert.IsNull(result.Error);
    }

    [TestMethod]
    public void ResultFailWithMessagesContainsMessage()
    {
        var result = Result.Fail("Error1");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<Error>(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultFailWithErrorsContainsErrors()
    {
        var result = Result.Fail(new Error("Error1"));
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Result>(result);
        Assert.IsNotInstanceOfType(result, typeof(Result<>));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<Error>(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultOkGenericIsSuccessAndNotFailed()
    {
        var result = Result.Ok<bool>();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.IsNull(result.Error);
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
        Assert.IsNull(result.Error);
    }

    [TestMethod]
    public void ResultFailGenericIsFailedAndNotSuccess()
    {
        var result = Result.Fail<bool>();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Error);
    }

    [TestMethod]
    public void ResultFailGenericWithErrorsContainsMessages()
    {
        var result = Result.Fail<bool>("Error1");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<Error>(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultFailGenericWithErrorsContainsErrors()
    {
        var result = Result.Fail<bool>(new Error("Error1"));
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<bool>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<Error>(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultNotNullWithValueReturnsSuccess()
    {
        var value = new object();
        var result = Result.NotNull(value, "Error1");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<object>));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.AreEqual(value, result.Value);
        Assert.IsNotNull(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultNotNullWithNullReturnsFailed()
    {
        object? value = null;
        var result = Result.NotNull(value, "Error1");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(Result<object?>));
        Assert.IsTrue(result.Failed);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(value, result.Value);
        Assert.IsNotNull(result.Error);
        Assert.IsInstanceOfType<Error>(result.Error);
        Assert.AreEqual("Error1", result.Error.Message);
    }

    [TestMethod]
    public void ResultCollectionIsCorrectlyFormedWithoutValues()
    {
        var handover = Result.Handover();
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(ResultCollection));
        Assert.IsNull(handover.Error);
        Assert.IsTrue(handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(0, handover.Value.Length);
    }

    [TestMethod]
    public void ResultCollectionIsCorrectlyFormedWithValues()
    {
        var handover = Result.Handover(1, 2, 3);
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(ResultCollection));
        Assert.IsNull(handover.Error);
        Assert.IsTrue(handover.Success);
        Assert.IsInstanceOfType<Array>(handover.Value);
        Assert.IsInstanceOfType<object?[]>(handover.Value);
        Assert.AreEqual(3, handover.Value.Length);
        Assert.AreEqual(1, handover.Value[0]);
        Assert.AreEqual(2, handover.Value[1]);
        Assert.AreEqual(3, handover.Value[2]);
    }

    [TestMethod]
    public void ResultCollectionAcceptsResult()
    {
        var handover = Result.Handover(Result.Ok(), null, Result.Fail());
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(ResultCollection));
        Assert.IsNull(handover.Error);
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
    public void ResultCollectionAcceptsNonResult()
    {
        var handover = Result.Handover(new object(), new object());
        Assert.IsNotNull(handover);
        Assert.IsInstanceOfType(handover, typeof(ResultCollection));
        Assert.IsNull(handover.Error);
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
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.IsNull(result.Error);
    }

    [TestMethod]
    public void FailFastWithNoFailedCallsReturnsSuccess()
    {
        var resultArray = new Result[3];

        var result = Result.FailFast([
            () => resultArray[0] = Result.Ok(),
            () => resultArray[1] = Result.Ok(),
            () => resultArray[2] = Result.Ok()
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        Assert.IsTrue(result.Success);
        Assert.IsFalse(result.Failed);
        Assert.IsNull(result.Error);
        var results = result.Results;
        Assert.AreEqual(resultArray[0], results[0]);
        Assert.AreEqual(resultArray[1], results[1]);
        Assert.AreEqual(resultArray[2], results[2]);
        Assert.AreEqual(3, result.Length);
    }

    [TestMethod]
    public void FailFastWithFailedCallsReturnsFail()
    {
        var resultArray = new Result[3];

        var result = Result.FailFast([
            () => resultArray[0] = Result.Ok(),
            () => resultArray[1] = Result.Fail("Error 1"),
            () => resultArray[2] = Result.Ok()
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Failed);
        Assert.IsNull(result.Error);
        var results = result.Results;
        Assert.AreEqual(resultArray[0], results[0]);
        Assert.AreEqual(resultArray[1], results[1]);
        Assert.AreEqual(results[1].Error.Message, "Error 1");
        Assert.AreEqual(2, result.Length);
    }

    [TestMethod]
    public void FailSafeWithNoCallsReturnsEmpty()
    {
        var result = Result.FailSafe();
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void FailSafeWithNoFailedCallsExecutesAllAndReturnsResults()
    {
        var resultArray = new Result[3];

        var result = Result.FailSafe([
            () => resultArray[0] = Result.Ok(),
            () => resultArray[1] = Result.Ok(),
            () => resultArray[2] = Result.Ok()
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        var results = result.Results;
        Assert.AreEqual(resultArray[0], results[0]);
        Assert.AreEqual(resultArray[1], results[1]);
        Assert.AreEqual(resultArray[2], results[2]);
        Assert.AreEqual(3, result.Length);
        Assert.IsTrue(results.All(r => r.Success));
    }

    [TestMethod]
    public void FailSafeWithFailedCallsExecutesAllAndReturnsResults()
    {
        var resultArray = new Result[4];

        var result = Result.FailSafe([
            () => resultArray[0] = Result.Ok(),
            () => resultArray[1] = Result.Fail("Error2"),
            () => resultArray[2] = Result.Fail("Error3"),
            () => resultArray[3] = Result.Ok()
        ]);

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(ResultCollection));
        Assert.AreEqual(4, result.Length);
        var results = result.Results;
        Assert.AreEqual(resultArray[0], results[0]);
        Assert.AreEqual(resultArray[1], results[1]);
        Assert.AreEqual(resultArray[2], results[2]);
        Assert.AreEqual(resultArray[3], results[3]);
        Assert.IsTrue(results[0].Success);
        Assert.IsFalse(results[1].Success);
        Assert.IsFalse(results[2].Success);
        Assert.IsTrue(results[3].Success);
    }
}