// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;

namespace carlist
{
    class Program
    {
        static void Main(string[] args)
        {
            String harga = "RM000";
            
            IWebDriver driver = new ChromeDriver("C:\\Drivers");
            // This will open up the URL
            driver.Url = "https://carlist.my/";
            driver.FindElement(By.XPath("//form/div/div/div")).Click();
            driver.FindElement(By.XPath("//div[contains(@data-value,'used')]")).Click();
            driver.FindElement(By.CssSelector(".search-button > .btn")).Click();
            driver.FindElement(By.XPath("//*[@id='classified-listings-result']/article[1]/div/div/h2/a")).Click();
            harga=driver.FindElement(By.XPath("//div[starts-with(@class,'listing__price')]")).Text;
            String[] hargadua = harga.Split(" ");
            String hargaFinal = hargadua[1];
            int hargaInt = int.Parse(hargaFinal, NumberStyles.AllowThousands);
            Console.Out.WriteLine("Car Price : " + hargaInt);
            if (hargaInt > 1000)
            {
                Console.Out.WriteLine("Passed : Car Price is Greater Than $1000");
            }
            else {
                Console.Out.WriteLine("Failed : Car Price is Not Greater Than 1000");
                    };
            
        }
    }
}
