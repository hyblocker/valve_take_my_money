using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace valve_take_my_money {
    public class WebPageDriver {

        private string m_url;
        private IWebDriver m_driver;

        public WebPageDriver(string url) {
            m_url = url;
            // Create headless browser
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-crash-reporter");
            options.AddArgument("--disable-in-process-stack-traces");
            options.AddArgument("--log-level=3");
            options.AddArgument("--disable-logging");
            m_driver = new ChromeDriver(options);

            // Navigate to URL
            m_driver.Navigate().GoToUrl(m_url);
        }

        public void Refresh() {
            m_driver.Navigate().Refresh();
        }

        public void Quit() {
            m_driver.Quit();
        }

        public IWebDriver Driver { get { return m_driver; } }
    }
}
