using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using TechTalk.SpecFlow;

namespace SnowDynamics
{
    [Binding]
    public class LoginSteps
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        Browser xrmBrowser;
        //public static Browser xrmBrowser = null;
        private readonly TestBase browserInstance;

        public LoginSteps(TestBase browserInstance)
        {
            this.browserInstance = browserInstance;
        }
        [Given(@"I login to CRM")]
        public void GivenILoginToCRM()
        {
            browserInstance.xrmBrowser = new Browser(TestSettings.Options);
            xrmBrowser = browserInstance.xrmBrowser;
            xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
            xrmBrowser.GuidedHelp.CloseGuidedHelp();
        }



        [When(@"I navigate to Sales and select Accounts")]
        public void WhenINavigateToSalesAndSelectAccounts()
        {
            xrmBrowser.ThinkTime(500);
            xrmBrowser.Navigation.OpenSubArea("Sales", "Companies");
        }

        [Then(@"I click on New command")]
        public void ThenIClickOnNewCommand()
        {
            xrmBrowser.ThinkTime(2000);
            xrmBrowser.Grid.SwitchView("Active Accounts");

            xrmBrowser.ThinkTime(1000);
            xrmBrowser.CommandBar.ClickCommand("New");
        }
    }
}
