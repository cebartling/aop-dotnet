using System.Configuration;

namespace UnityAopSpike.AcceptanceTests
{
    public class AcceptanceTestingSection : ConfigurationSection
    {
        [ConfigurationProperty("baseUrl", DefaultValue = "http://localhost:55667", IsRequired = true)]
        public string BaseUrl
        {
            get
            {
                return (string)this["baseUrl"];
            }
            set
            {
                this["baseUrl"] = value;
            }
        }

    }
}