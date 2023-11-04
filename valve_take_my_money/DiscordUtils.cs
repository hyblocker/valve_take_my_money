using Newtonsoft.Json;
using System.Text;

namespace valve_take_my_money {
    public static class DiscordUtils {
        private static HttpClient? s_client;
        public static HttpClient Client {
            get {
                if ( s_client == null )
                    s_client = new HttpClient();

                return s_client;
            }
        }

        public static void SendWebhookMessage(string message) {
            const string WEBHOOK_URL = "SET_WEBHOOK_URL_HERE";

            var webhookObject = new
            {
                username = "John Capitalism",
                content = $"<@SET_YOUR_USER_ID_HERE> {message}",
                avatar_url = "https://cdn.discordapp.com/avatars/931975517677191189/513e695121c1f3e48f25a5812c52b12d.png?size=4096"
            };

            var content = new StringContent(JsonConvert.SerializeObject(webhookObject), Encoding.UTF8, "application/json");

            Client.PostAsync(WEBHOOK_URL, content).Wait();
        }
    }
}
