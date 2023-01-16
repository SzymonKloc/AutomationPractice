using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automationPractice.Pages
{
    internal class BasePage
    {
        public IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement FindElement(By locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(500);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(driver => driver.FindElement(locator));
        }

        public IList<IWebElement> FindElements(By locator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(500);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(driver => driver.FindElements(locator));
        }
    }
}
