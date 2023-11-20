﻿using DemoTestFramework.Selenium.Pages;
using DemoTestFramework.Tests;
using NUnit.Framework;
namespace DemoTestFramework.Selenium.Tests
{
    public class LoginPageTests : BaseTest
    {
        private readonly string homePageUrl = "http://eaapp.somee.com/";
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            InitDriver(homePageUrl);
            loginPage = new LoginPage(driver);
        }

        [TestCase("http://eaapp.somee.com/Account/Login", "Login - Execute Automation Employee App")]
        public void HasCorrectUrlAndTitle(string url, string title)
        {
            var loginPage = NavigateToLoginPage();

            Assert.AreEqual(url, loginPage.HasCorrectUrl(), "Page URL isn't correct.");
            Assert.AreEqual(title, loginPage.HasCorrectPageTitle(), "Page title isn't correct.");
        }

        [TestCase("admin", "password", "Hello admin!")]
        public void CanUserLogin(string userName, string password, string expectedLnkText)
        {
            var loginPage = NavigateToLoginPage();
            var loggedInPage = loginPage.PerformLogin(userName, password);

            Assert.IsNotNull(loggedInPage, "Logged-in page could not be found.");
            Assert.IsTrue(loggedInPage.HasLnkHelloAdmin, "Logged-in page 'Hello Admin' link could not be found.");
            Assert.AreEqual(expectedLnkText, loggedInPage.LnkHelloAdminText, "'Hello Admin' signatuse isn't correct.");
        }

        [TestCase("", "password", "The UserName field is required.")]
        public void IsUserNameFldErrorMsgDisplayed(string userName, string password, string expectedErrMsg)
        {
            var loginPage = NavigateToLoginPage();
            loginPage.PerformLogin(userName, password);

            Assert.AreEqual(expectedErrMsg, loginPage.TxtUserNameErrorMsgText, "User name text field error message text signatuse isn't correct.");
        }

        [TestCase("admin", "", "The Password field is required.")]

        public void IsPasswordFldErrorMsgDisplayed(string userName, string password, string expectedErrMsg)
        {
            var loginPage = NavigateToLoginPage();
            loginPage.PerformLogin(userName, password);

            Assert.AreEqual(expectedErrMsg, loginPage.TxtPasswordErrorMsgText, "User name text field error message text signatuse isn't correct.");
        }

        #region Helper methods

        private LoginPage NavigateToLoginPage()
        {
            HomePage homePage = new HomePage(driver);
            var loginPage = homePage.LnkLoginClick();
            return loginPage;
        }

        #endregion
    }
}