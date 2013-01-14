namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Diagnostics;

    using NUnit.Framework;

    using OpenQA.Selenium;

    public abstract class SeleniumTestBase
    {
        private readonly string driverName;

        private Stopwatch stopwatch = new Stopwatch();

        protected SeleniumTestBase(string driverName)
        {
            this.driverName = driverName;
        }

        public IWebDriver Driver { get; set; }

        [SetUp]
        public void LoadDriver()
        {
            this.stopwatch.Start();
            if (this.Driver == null)
            {
                this.Driver = SeleniumFactory.GetInstance().CreateDriver(this.driverName);
            }
        }

        [TearDown]
        public void CloseDriver()
        {
            this.stopwatch.Stop();
            Console.WriteLine("Took: " + this.stopwatch.Elapsed.TotalSeconds + "s");
            this.stopwatch.Reset();
            
            if (this.Driver == null)
            {
                return;
            }

            this.Driver.Close();
            this.Driver = null;
        }
    }
}