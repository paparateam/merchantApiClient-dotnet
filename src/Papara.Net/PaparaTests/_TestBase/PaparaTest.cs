using Newtonsoft.Json;
using Papara;
using System.IO;

namespace PaparaTests
{
    public class PaparaTest
    {
        public RequestOptions Options { get; }
        public AppSettings Settings { get; }

        public PaparaTest()
        {
            this.Settings = this.GetConfiguration();

            this.Options = new RequestOptions
            {
                ApiKey = this.Settings.Papara.ApiKey,
                Env = this.Settings.Papara.Env
            };
        }

        public AppSettings GetConfiguration()
        {
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + "/appsettings.test.json"))
            {
                var appSettings = JsonConvert.DeserializeObject<AppSettings>(reader.ReadToEnd());
                return appSettings;
            }
        }
    }
}
