using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V106.SystemInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace automationPractice.DTO
{
    public class ProductTile
    {

        public string Name { get; set; }
        public string RatingStarsInPercent { get; set; }
        public string NumberOfReviews { get; set; }
        public string Price { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Colors { get; set; }

        public ProductTile() { }
        public ProductTile(IWebElement productTile)
        {
            //All products have name and price
            Name = productTile.FindElement(By.XPath("//div/strong/a")).Text;
            Price = productTile.FindElement(By.CssSelector(".price")).Text;

            try
            {
                IWebElement ratingStarsElement = productTile.FindElement(By.CssSelector(".rating-result span span"));
                if (ratingStarsElement != null)
                {
                    RatingStarsInPercent = ratingStarsElement.Text;
                }

                IWebElement NumberOfReviewsElement = productTile.FindElement(By.CssSelector(".reviews-actions a"));
                if (NumberOfReviewsElement != null)
                {
                    NumberOfReviews = NumberOfReviewsElement.Text;
                }

                //Sizes - why is it saved as a script?
                IWebElement tileScript= productTile.FindElement(By.CssSelector("script"));
                if(tileScript != null)
                {
                    var scriptValue = tileScript.GetAttribute("innerHTML");

                    var patternSizes = "(?<=\"value\":\")[A-Z]{1,2}(?=\")";
                    var regexSizes = new Regex(patternSizes);
                    var sizesGroup = regexSizes.Matches(scriptValue);
                    var sizesList = new List<string>();
                    foreach(var size in sizesGroup)
                    {
                        sizesList.Add(size.ToString());
                    }
                    Sizes = sizesList;

                    //Colors
                    var patternColors = "(?<=\"#.{6}\",\"label\":\")[A-Z][a-z]{2,}(?=\")";
                    var regexColors = new Regex(patternColors);
                    var colorsGroup = regexColors.Matches(scriptValue);
                    var colorsList = new List<string>();
                    foreach(var color in colorsGroup)
                    {
                        colorsList.Add(color.ToString());
                    }
                    Colors = colorsList;
                }
            }
            catch (NoSuchElementException) { }
        }
    }
}
