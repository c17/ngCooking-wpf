using Newtonsoft.Json;
using System;

namespace apis.Client.Models
{
    public class User
    {
        [JsonProperty("firstname")]
        public String FirstName { get; set; }

        [JsonProperty("surname")]
        public String LastName { get; set; }

        [JsonProperty("level")]
        public Int32 Level { get; set; }

        [JsonProperty("city")]
        public String City { get; set; }

        [JsonProperty("birth")]
        public Int16 BirthYear { get; set; }

        [JsonProperty("bio")]
        public String Bio { get; set; }

        [JsonProperty("picture")]
        public String Picture { get; set; }
    }
}
