using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automationPractice.Pages
{
    internal class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }

         IList<IWebElement> productsTilesTitles => driver.FindElements(By.CssSelector("li.product-item .product-item-name a"));

        public List<string> SearchResultTitles()
        {
            var productsTitles = new List<string>();
            foreach(var link in productsTilesTitles)
            {
                productsTitles.Add(link.Text);
            }
            return productsTitles;
        }

    }
}
