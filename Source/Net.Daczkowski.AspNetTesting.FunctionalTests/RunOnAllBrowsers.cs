﻿namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using NUnit.Framework;

    [TestFixture("FirefoxDriver")]
    [TestFixture("InternetExplorerDriver")]
    [TestFixture("ChromeDriver")]
    [TestFixture("PhantomJsDriver")]
    public abstract class RunOnAllBrowsers : SeleniumTestBase
    {
        protected RunOnAllBrowsers(string driverName)
            : base(driverName)
        {
        }
    }
}