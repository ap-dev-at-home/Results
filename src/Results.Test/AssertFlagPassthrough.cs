namespace Results.Test;

internal class AssertFlagPassthrough
{
    public bool Flag { get; private set; }

    public void Assert(Func<bool> func)
    {
        this.Flag = true;
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(true, func());
    }

    public void Assert(bool flag)
    {
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(flag, this.Flag);
    }
}
