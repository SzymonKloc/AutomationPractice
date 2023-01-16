using AngleSharp.Dom;
using automationPractice.DTO;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V106.Page;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automationPractice.Pages
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        IWebElement btnShopNewYoga => FindElement(By.CssSelector(".action.more.button"));
        IWebElement linkShopPants => FindElement(By.CssSelector(".home-pants"));
        IWebElement linkShopErinRecommendations => FindElement(By.CssSelector(".home-erin"));
        IWebElement linkShopTees => FindElement(By.CssSelector(".home-t-shirts"));
        IWebElement linkShopPerformance => FindElement(By.CssSelector(".home-performance"));
        IWebElement linkShopEco => FindElement(By.CssSelector(".home-eco"));
        IWebElement linkSignIn => FindElement(By.CssSelector(".page-header .authorization-link a"));
        IWebElement linkSignUp => FindElement(By.LinkText("Create an Account"));
        IWebElement linkWhatsNew => FindElement(By.CssSelector(".nav-sections a[href$='/what-is-new.html']"));
        IWebElement liWomen => FindElement(By.CssSelector(".nav-sections a[href$='/women.html']"));
        IWebElement liTops => FindElement(By.CssSelector(".nav-sections a[href$='/tops-women.html']"));
        IWebElement searchBar => FindElement(By.Id("search"));
        IList<IWebElement> productsTiles => FindElements(By.CssSelector(".product-item-details"));
        IWebElement newsLetterInputField => FindElement(By.CssSelector("#newsletter"));
        IWebElement btnSubscribe => FindElement(By.CssSelector(".action.subscribe.primary"));
        IWebElement msgSuccess => FindElement(By.CssSelector(".message-success"));


        public void ClickBtnShopNewYoga() => btnShopNewYoga.Click();

        public void ClickLinkShopPants() => linkShopPants.Click();

        public void ClickLinkShopErinRecommendations() => linkShopErinRecommendations.Click();

        public void ClickLinkShopTees() => linkShopTees.Click();

        public void ClickLinkShopPerformance() => linkShopPerformance.Click();

        public void ClickLinkShopEco()  => linkShopEco.Click();

        public void ClickSignInLink() => linkSignIn.Click();
        
        public void ClickSignUpLink() => linkSignUp.Click();

        public void ClickWhatsNewLink() => linkWhatsNew.Click();

        public void ClickWomenLink() => liWomen.Click();

        public void ClickWomenTopsLi()
        {
            Actions action = new Actions(driver);
            
            action.MoveToElement(liWomen);
            //js executor? dlaczego nie czeka?
            Thread.Sleep(500);
            action.Click(liTops).Build().Perform();
        }

        public void Search(string phrase)
        {
            searchBar.SendKeys(phrase);
            searchBar.Submit();
        }

        public List<ProductTile> GetProducts()
        {
            List<ProductTile> products = new List<ProductTile>();
            foreach (IWebElement tile in productsTiles)
            {
                products.Add(new ProductTile(tile));
            }
            return products;
        }

        public void EnterEmailForNewsletter(string email)
        {
            newsLetterInputField.SendKeys(email);
        }

        public void SubscribeToNewsLetter() => btnSubscribe.Click();

        public bool SuccessMsgDisplayed() => msgSuccess.Displayed;
    }
}
