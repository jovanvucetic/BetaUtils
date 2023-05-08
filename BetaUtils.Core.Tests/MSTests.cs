using BetaUtils.Core.Configuration;

namespace BetaUtils.Core.Tests;

[TestClass]
public class MsTests
{
    [TestMethod]
    public void Init()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Init().InitialTestClass());
    }
}