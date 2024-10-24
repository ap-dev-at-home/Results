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
}