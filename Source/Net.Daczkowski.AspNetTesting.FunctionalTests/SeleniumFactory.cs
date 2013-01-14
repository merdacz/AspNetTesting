namespace Net.Daczkowski.AspNetTesting.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;

    public class SeleniumFactory
    {
        private static readonly SeleniumFactory Instance = new SeleniumFactory();

        private readonly IList<IWebDriver> registered = new List<IWebDriver>();

        private InternetExplorerDriverService internetExplorerDriverService;

        private ChromeDriverService chromeDriverService;

        private SeleniumFactory()
        {
        }

        public static SeleniumFactory GetInstance()
        {
            return Instance;
        }

        public IWebDriver CreateDriver(string driverName)
        {
            IWebDriver driver;
            switch (driverName)
            {
                case "FirefoxDriver":
                    driver = new FirefoxDriver();
                    break;
                case "InternetExplorerDriver":
                    if (this.internetExplorerDriverService == null)
                    {
                        this.internetExplorerDriverService =
                            InternetExplorerDriverService.CreateDefaultService(GetDriverDirectory());
                        this.internetExplorerDriverService.Start();
                    }

                    driver = new RemoteWebDriver(
                        this.internetExplorerDriverService.ServiceUrl, DesiredCapabilities.InternetExplorer());
                    break;
                case "ChromeDriver":
                    if (this.chromeDriverService == null)
                    {
                        this.chromeDriverService = ChromeDriverService.CreateDefaultService(GetDriverDirectory());
                        this.chromeDriverService.Start();
                    }

                    driver = new RemoteWebDriver(this.chromeDriverService.ServiceUrl, DesiredCapabilities.Chrome());
                    break;
                default:
                    throw new NotImplementedException("Unsupported Selenium web driver. ");
            }

            driver.Manage().Window.Maximize();
            this.registered.Add(driver);
            return driver;
        }

        public void QuitAll()
        {
            if (this.chromeDriverService != null)
            {
                this.chromeDriverService.Dispose();
            }

            if (this.internetExplorerDriverService != null)
            {
                this.internetExplorerDriverService.Dispose();
            }
        }

        private static string GetDriverDirectory()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var packagesDir = LocateDir(currentDir, "tools");

            return new DirectoryInfo(packagesDir).FullName;
        }

        private static string LocateDir(string currentDir, string dirToFind)
        {
            if (currentDir.ToLower().Contains(dirToFind))
            {
                return currentDir;
            }

            var dirPath = new DirectoryInfo(currentDir).Parent.FullName;
            string[] dirs = Directory.GetDirectories(dirPath);

            foreach (string dir in dirs)
            {
                if (dir.ToLower().Contains(dirToFind))
                {   
                    return dir;
                }
            }
            
            return LocateDir(dirPath, dirToFind);
        }
    }
}