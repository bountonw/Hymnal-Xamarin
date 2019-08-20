using Newtonsoft.Json;

namespace Hymnal.Core.Models
{
    public class Hymn
    {
        [JsonProperty("id")]
        public string NumberString { get; set; }

        public int Number => int.Parse(NumberString);

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_plain")]
        public string TitlePlain { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
