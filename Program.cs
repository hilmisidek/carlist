// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using NUnit.Framework;
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//*[@id='classified-listings-result']/article[1]/div/div/h2/a")).Click(); //click on 1st result
            harga = driver.FindElement(By.XPath("//div[starts-with(@class,'listing__price')]")).Text; //capture the price
            Assert.That(harga, Does.StartWith("RM"),"Blocked no price"); //test the price, sometime 1st result show no price                     
            Console.Out.WriteLine("Car Price :" + harga); // print the captured price in integer 
            int hargaIntFinal = GetHarga(harga); //call method to convert Price to Integer
            Assert.That(hargaIntFinal, Is.GreaterThan(1000)); //Assert price more than 1000
            Console.Out.WriteLine("Passed: Price is more than RM1,000"); //print output
            driver.Close();
            return;
        }
        static int GetHarga(String harga)
        {
            String[] hargadua = harga.Split(" "); //split RM with number
            String hargaFinal = hargadua[1]; //take the second portion of the split
            int hargaInt = int.Parse(hargaFinal, NumberStyles.AllowThousands); //convert to integer recognize thousand separator
            return hargaInt;
        }



    }
}