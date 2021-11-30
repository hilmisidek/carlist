// See https://aka.ms/new-console-template for more information
using Ocelot.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace carlist
{
    class Program
    {
        static void Main(string[] args)
        {
            String harga = "RM000";
            
            IWebDriver driver = new ChromeDriver("C:\\Driver");
            driver.Url = "https://carlist.my/"; // This will open up the URL
            driver.FindElement(By.XPath("//form/div/div/div")).Click(); //click on dropdown list
            driver.FindElement(By.XPath("//div[contains(@data-value,'used')]")).Click(); //select "used"
            driver.FindElement(By.CssSelector(".search-button > .btn")).Click(); //click on search button

           // Wait.until(ExpectedConditions.visibilityOf(driver.FindElement(By.XPath("//*[@id='classified-listings-result']/article[1]/div/div/h2/a"))));
            driver.FindElement(By.XPath("//*[@id='classified-listings-result']/article[1]/div/div/h2/a")).Click(); //click on 1st result
            harga=driver.FindElement(By.XPath("//div[starts-with(@class,'listing__price')]")).Text; //capture the price
            String[] hargadua = harga.Split(" "); //split RM with number
            String hargaFinal = hargadua[1]; //take the second portion of the split
            int hargaInt = int.Parse(hargaFinal, NumberStyles.AllowThousands); //convert to integer recognize thousand separator
            Console.Out.WriteLine("Car Price : " + hargaInt); // print the captured price in integer
            int priceCompare = 1000; //value to assert price more than the value
            String result = assertion(hargaInt, priceCompare);
            Console.Out.WriteLine(result); //print output
            driver.Close();
            return;
        }

        static String assertion(int hargaFound, int valueAsssert)
        {
            if (hargaFound > valueAsssert)
            {
                return "Passed : Car Price is More Than RM1000";
            }
            else
            {
                return "Failed : Car Price is Not More Than RM1000";
            }
        }


    }
}
