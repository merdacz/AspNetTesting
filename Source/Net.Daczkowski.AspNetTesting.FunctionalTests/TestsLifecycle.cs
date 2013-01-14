namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System.Globalization;
    using System.Threading;

    using NUnit.Framework;

    [SetUpFixture]
    public class TestsLifecycle
    {
        [SetUp]
        public void InitializeGlobalization()
        {
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
        }

        [TearDown]
        public void QuitDrivers()
        {
            SeleniumFactory.GetInstance().QuitAll();
        }
    }
}