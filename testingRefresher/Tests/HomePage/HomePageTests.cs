using AngleSharp;
using AngleSharp.Dom;
using automationPractice.DTO;
using automationPractice.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;

namespace automationPractice.Tests.HomePage
{
    internal class HomePageTests : BaseTest
    {
        Pages.HomePage homePage;

        [SetUp]
        public void SetUp()
        {
            homePage = new Pages.HomePage(driver);
        }

        [Test, Category("Banners")]
        public void YogaBannerBtnRedirection()
        {
            homePage.ClickBtnShopNewYoga();

            var destinationURL = PagesUrls.HomePage + PagesUrls.YogaCollection;
            var actualUrl = driver.Url;

            Assert.That(destinationURL, Is.EqualTo(actualUrl));
        }


        [Test, Category("Banners")]
        public void PantsBannerLinkRedirection()
        {
            homePage.ClickLinkShopPants();

            var destinationURL = PagesUrls.HomePage + PagesUrls.PantsPromotion;
            var actualUrl = driver.Url;

            Assert.That(destinationURL, Is.EqualTo(actualUrl));
        }

        [Test, Category("Banners")]
        public void TeesBannerLinkRedirection()
        {
            homePage.ClickLinkShopTees();

            var destinationURL = PagesUrls.HomePage + PagesUrls.TeesPromotion;
            var actualUrl = driver.Url;

            Assert.That(destinationURL, Is.EqualTo(actualUrl));
        }

        [Test, Category("Banners")]
        public void ErinRecommendationsBannerLinkRedirection()
        {
            homePage.ClickLinkShopErinRecommendations();

            var destinationURL = PagesUrls.HomePage + PagesUrls.ErinRecommendationsCollection;
            var actualUrl = driver.Url;

            Assert.That(destinationURL, Is.EqualTo(actualUrl));
        }

        [Test, Category("Banners")]
        public void PerformanceBannerLinkRedirection()
        {
            homePage.ClickLinkShopPerformance();

            var destinationURL = PagesUrls.HomePage + PagesUrls.PerformanceCollection;
            var actualUrl = driver.Url;

            Assert.That(destinationURL, Is.EqualTo(actualUrl));
        }

        [Test, Category("Header")]
        public void SignInLinkRedirection()
        {
            homePage.ClickSignInLink();

            var pageUrl = driver.Url;
            var expectedUrl = PagesUrls.HomePage + PagesUrls.SignInPage;

            Assert.That(pageUrl, Is.EqualTo(expectedUrl));
        }

        [Test, Category("Header")]
        public void SignUpLinkRedirection()
        {
            homePage.ClickSignUpLink();

            var pageUrl = driver.Url;
            var expectedUrl = PagesUrls.HomePage + PagesUrls.SignUpPage;

            Assert.That(pageUrl, Is.EqualTo(expectedUrl));
        }

        [Test, Category("Navigation")]
        public void WhatsNewRedirection()
        {
            homePage.ClickWhatsNewLink();

            var pageUrl = driver.Url;
            var expectedUrl = PagesUrls.HomePage + PagesUrls.WhatIsNewPage;

            Assert.That(pageUrl, Is.EqualTo(expectedUrl));
        }

        [Test, Category("Navigation")]
        public void WomenCategoryRedirection()
        {
            homePage.ClickWomenLink();

            var pageUrl = driver.Url;
            var expectedUrl = PagesUrls.HomePage + PagesUrls.WomenPage;

            Assert.That(pageUrl, Is.EqualTo(expectedUrl));
        }

        [Test, Category("Navigation")]
        public void TopsListItemRedirection()
        {
            homePage.ClickWomenTopsLi();

            var pageUrl = driver.Url;
            var expectedUrl = PagesUrls.HomePage + PagesUrls.WomenTopsPage;

            Assert.That(pageUrl, Is.EqualTo(expectedUrl));
        }

        [Test, Category("Products")]
        public void SearchForProduct()
        {
            var searchPhrase = "Hero Hoodie";

            homePage.Search(searchPhrase);

            var searchPage = new SearchResultsPage(driver);
            var titles = searchPage.SearchResultTitles();

            Assert.That(searchPhrase, Is.EqualTo(titles[0]));
        }


        [Test, Category("Products")]
        public void HotSellersDisplayed()
        {
            var hotSellers = homePage.GetProducts();

            Assert.That(hotSellers.Count, Is.AtLeast(1));
            foreach (var product in hotSellers)
            {
                Assert.That(product.Name, Is.Not.Empty, "Empty string in product name");
                Assert.That(product.Price.Contains('$'), "No currency ($) symbol in price");
            }
        }

        private static readonly ProductTile Repository = GetRepository<ProductTile>("Tests\\HomePage\\HomePageProductData.json");

        [Test, Category("Products")]
        public void ProductTileHasAllElements()
        {
            var firstHotSeller = homePage.GetProducts()[0];
            Assert.Multiple(() =>
            {
                Assert.That(firstHotSeller.Name, Is.EqualTo(Repository.Name));
                Assert.That(firstHotSeller.RatingStarsInPercent, Is.EqualTo(Repository.RatingStarsInPercent));
                Assert.That(firstHotSeller.NumberOfReviews, Is.EqualTo(Repository.NumberOfReviews));
                Assert.That(firstHotSeller.Price, Is.EqualTo(Repository.Price));
                Assert.That(firstHotSeller.Sizes, Is.EquivalentTo(Repository.Sizes));
                Assert.That(firstHotSeller.Colors, Is.EquivalentTo(Repository.Colors));
            });
        }

        [Test, Category("Footer")]
        public void EnrollToNewsletter()
        {
            var email = $"john@doe{DateTime.Now.Ticks}.com";
            homePage.EnterEmailForNewsletter(email);
            homePage.SubscribeToNewsLetter();

            Assert.That(homePage.SuccessMsgDisplayed);
        }
    }
}
