using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace automationPractice.Tests
{
    internal class BaseTest
    {
        public IWebDriver driver;

        protected static T GetRepository<T>(string partialPath)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, partialPath);
            var repo = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return repo;
        }

        [SetUp]
        public void SetUpTest()
        {
            var browserName = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browserName);
            driver.Manage().Window.Maximize();
            driver.Url= ConfigurationManager.AppSettings["url"];
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        private void InitBrowser(string browserName)
        {
            switch(browserName)
            {
                case "Chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "Firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "Edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }
        }
    }
}
