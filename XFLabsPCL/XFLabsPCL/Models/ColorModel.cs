namespace XFLabsPCL.Models
{
    using Newtonsoft.Json;

    public class ColorModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("pantone_value")]
        public string PantoneValue { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }
    }
}
