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
    public class RegistroDeDatos
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
        public void TheRegistroDeDatosTest()
        {
            driver.Navigate().GoToUrl("https://katalon-test.s3.amazonaws.com/demo-aut/dist/html/form.html");
            driver.FindElement(By.Id("submit")).Click();
            driver.FindElement(By.Id("first-name")).Click();
            driver.FindElement(By.Id("first-name")).Clear();
            driver.FindElement(By.Id("first-name")).SendKeys("Gerardo");
            driver.FindElement(By.Id("last-name")).Clear();
            driver.FindElement(By.Id("last-name")).SendKeys("Ramos");
            driver.FindElement(By.Name("gender")).Click();
            driver.FindElement(By.Id("dob")).Click();
            driver.FindElement(By.XPath("//tr[3]/td[6]")).Click();
            driver.FindElement(By.Id("address")).Click();
            driver.FindElement(By.Id("address")).Clear();
            driver.FindElement(By.Id("address")).SendKeys("Calle 37 Sur 35-65");
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("lgerardoramos@hotmail.com");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("1234");
            driver.FindElement(By.Id("company")).Click();
            driver.FindElement(By.Id("company")).Clear();
            driver.FindElement(By.Id("company")).SendKeys("MSFT");
            driver.FindElement(By.Id("role")).Click();
            new SelectElement(driver.FindElement(By.Id("role"))).SelectByText("Business Analyst");
            driver.FindElement(By.Id("role")).Click();
            // ERROR: Caught exception [ERROR: Unsupported command [addSelection | id=expectation | label=Excellent colleagues]]
            driver.FindElement(By.XPath("//select[@id='expectation']/option[3]")).Click();
            driver.FindElement(By.XPath("//form[@id='infoForm']/div[11]/div/div/label")).Click();
            driver.FindElement(By.XPath("//form[@id='infoForm']/div[11]/div/div[3]/label")).Click();
            driver.FindElement(By.XPath("//form[@id='infoForm']/div[11]/div/div[5]/label")).Click();
            driver.FindElement(By.Id("comment")).Click();
            driver.FindElement(By.Id("comment")).Clear();
            driver.FindElement(By.Id("comment")).SendKeys("Todo ok");
            // ERROR: Caught exception [ERROR: Unsupported command [captureEntirePageScreenshot | before-submit-button-click | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | 1 | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [label | SUMAR | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | ${i}+1 | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [gotoIf | i<5 | SUMAR]]
            // ERROR: Caught exception [ERROR: Unsupported command [gotoLabel | FINAL | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [if | i==1 | ]]
            driver.FindElement(By.Id("first-name")).Click();
            driver.FindElement(By.Id("first-name")).Clear();
            driver.FindElement(By.Id("first-name")).SendKeys("Gerardo1");
            // ERROR: Caught exception [ERROR: Unsupported command [endIf |  | ]]
            driver.FindElement(By.Id("submit")).Click();
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.Id("submit-msg")).Text, ".*submitted!"));
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            // ERROR: Caught exception [ERROR: Unsupported command [captureEntirePageScreenshot | after-submit-button-click | ]]
            String NombreUsuario = "Gerardo";
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | new Date().toString() | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | 1 | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | i+j | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [runScript | return window.$.fn.jquery | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [while | j < 5 | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | i+j | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [getEval | $(j)+1 | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [endWhile |  | ]]
            // ERROR: Caught exception [ERROR: Unsupported command [label | FINAL | ]]
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
