using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class Verify
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheVerifyTest()
        {
            driver.Navigate().GoToUrl("https://katalon-test.s3.amazonaws.com/demo-aut/dist/html/form.html");
            driver.FindElement(By.Id("first-name")).Click();
            driver.FindElement(By.Id("first-name")).Clear();
            driver.FindElement(By.Id("first-name")).SendKeys("Alex");
            driver.FindElement(By.Id("last-name")).Clear();
            driver.FindElement(By.Id("last-name")).SendKeys("Smith");
            driver.FindElement(By.Id("submit")).Click();
            try
            {
                Assert.AreEqual("This field is required.", driver.FindElement(By.Id("gender-error")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.Id("dob-error")).Text, "^[\\s\\S]*required[\\s\\S]*$"));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.Id("address-error")).Text, ".*required\\."));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
