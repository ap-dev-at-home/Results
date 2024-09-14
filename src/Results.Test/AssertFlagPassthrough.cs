namespace Results.Test;

internal class AssertFlagPassthrough
{
    public bool Flag { get; private set; } = false;

    public int Count { get; private set; } = 0;

    public void Assert(Func<bool> func)
    {
        this.Flag = true;
        this.Count++;
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(true, func());
    }

    public void Assert(bool flag, int count)
    {
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(flag, this.Flag);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(count, this.Count);
    }
}
