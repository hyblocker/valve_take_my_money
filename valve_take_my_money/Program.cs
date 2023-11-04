using OpenQA.Selenium;
using System.Diagnostics;

namespace valve_take_my_money {
    internal class Program {

        private const string STEAM_DECK_URL = "https://store.steampowered.com/sale/steamdeckrefurbished";
        private const int TIMEOUT = 20 * 1000; // 20s
        private const int TRIES = 10;
        private static WebPageDriver s_driver;

        static bool hasOpenedPage = false;

        public static bool TrySacrifice() {
            var all_btns = s_driver.Driver.FindElements(By.XPath("//*[@id='SaleSection_33131']"));

            var txt = all_btns.First().Text;
            txt = txt.Split("00")[0];
            Console.WriteLine($"{DateTime.Now} {txt.Replace("\r\n", "\t")}");

            bool status;

            if ( txt.ToLower().Contains("add") ) {
                DiscordUtils.SendWebhookMessage("DECK HARD DECK HARD DECK HARD DECK HARD DECK HARD DECK HARD DECK HARD DECK HARD DECK HARD");
                status = true;
                if ( !hasOpenedPage ) {
                    // open deck page on steam
                    Process.Start("steam://openurl/https://store.steampowered.com/sale/steamdeckrefurbished/");
                }
                hasOpenedPage = true;
            } else {
                status = false;
            }

            return status;
        }

        static bool CreateDriverSafely() {
            try {
                s_driver = new WebPageDriver(STEAM_DECK_URL);
                return true;
            } catch ( Exception ex ) {
                s_driver.Quit();
                Thread.Sleep(TIMEOUT);
                Process.Start(new ProcessStartInfo() {
                    CreateNoWindow = true,
                    FileName = "cmd",
                    Arguments = "/C taskkill /f /im chrome.exe"
                });
                Process.Start(new ProcessStartInfo() {
                    CreateNoWindow = true,
                    FileName = "cmd",
                    Arguments = "/C taskkill /f /im chromedriver.exe"
                });
            }
            return false;
        }

        static void PrayToGabenAllMighty() {
            int count = 0;
            if ( !CreateDriverSafely() ) {
                PrayToGabenAllMighty();
            }

            Console.WriteLine("Started the ritual...");
            while ( true ) {
                try {
                    if ( count <= TRIES ) {
                        bool status = TrySacrifice();
                        if ( status == true ) {
                            break;
                        }
                        count++;
                        s_driver.Refresh();
                        Thread.Sleep(TIMEOUT);
                    } else {
                        Console.WriteLine("Restarting...");
                        s_driver.Quit();
                        Process.Start(new ProcessStartInfo() {
                            CreateNoWindow = true,
                            FileName = "cmd",
                            Arguments = "/C taskkill /f /im chrome.exe"
                        });
                        Process.Start(new ProcessStartInfo() {
                            CreateNoWindow = true,
                            FileName = "cmd",
                            Arguments = "/C taskkill /f /im chromedriver.exe"
                        });
                        Thread.Sleep(TIMEOUT);
                        count = 0;
                        CreateDriverSafely();
                        PrayToGabenAllMighty();
                    }
                } catch ( Exception ex ) {
                    Console.WriteLine(ex.Message);
                    s_driver.Quit();
                    Thread.Sleep(TIMEOUT); // DO NOT EDIT
                    PrayToGabenAllMighty();
                }
            }
        }

        static void Main(string[] args) {
            PrayToGabenAllMighty();
        }
    }
}