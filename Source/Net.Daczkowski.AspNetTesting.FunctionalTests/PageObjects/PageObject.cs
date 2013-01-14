namespace Net.Daczkowski.AspNetTesting.FunctionalTests.PageObjects
{
    using System;
    using System.Configuration;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;

    public abstract class PageObject
    {
        protected PageObject(IWebDriver driver)
        {
            this.Driver = driver;
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
            var absoluteUrl = ConfigurationManager.AppSettings["BaseUrl"] + this.RelativeLocation;
            if (this.Driver.Url == absoluteUrl)
            {
                return;
            }

            this.Driver.Navigate().GoToUrl(absoluteUrl);
        }

        protected IWebDriver Driver { get; private set; }

        protected WebDriverWait Wait { get; private set; }

        protected abstract string RelativeLocation { get; }
    }
}