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
            IWebDriver Driver = new ChromeDriver("C:\\Driver");
            String webURL= "https://carlist.my/"; //initialize web address

            String harga = GetPriceText(webURL,Driver); //call method to get price in string
            
            Assert.That(harga, Does.StartWith("RM"),"Blocked no price"); //test the price, sometime 1st result show no price, only "call now"                     
            Console.Out.WriteLine("\nCar Price :" + harga); // print the captured price in String 
            
            int hargaIntFinal = GetHarga(harga); //call method to convert Price to Integer
            
            Assert.That(hargaIntFinal, Is.GreaterThan(1000)); //Assert price more than 1000
            Console.Out.WriteLine("Passed: Price is more than RM1,000"); //print output

            Driver.Close(); //close webdriver

            return; //stop execution
        }

        
        static String GetPriceText(String webURL, IWebDriver driver)
        { 
            String hargaText = "RM000"; //initialize
        
            //get all required element
            driver.Url = webURL; // This will open up the URL
            driver.FindElement(By.XPath("//form/div/div/div")).Click(); //click on dropdown list
            driver.FindElement(By.XPath("//div[contains(@data-value,'used')]")).Click(); //select "used"
            driver.FindElement(By.CssSelector(".search-button > .btn")).Click(); //click on search button
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); //wait for the element
            driver.FindElement(By.XPath("//*[@id='classified-listings-result']/article[1]/div/div/h2/a")).Click(); //click on 1st result
        
            hargaText = driver.FindElement(By.XPath("//div[starts-with(@class,'listing__price')]")).Text; //capture the price
        
            return hargaText; //return price in String
        }

        static int GetHarga(String harga) //method to covert string to integer
        {
            String[] hargadua = harga.Split(" "); //split RM with number
            String hargaFinal = hargadua[1]; //take the second portion of the split
            int hargaInt = int.Parse(hargaFinal, NumberStyles.AllowThousands); //convert to integer recognize thousand separator
            return hargaInt; //return final integer
        }



    }
}