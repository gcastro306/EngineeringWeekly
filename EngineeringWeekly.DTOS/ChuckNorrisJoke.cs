using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.DTOS
{
    [ExcludeFromCodeCoverage]
    public class ChuckNorrisJoke
    {
        [JsonProperty("categories")]
        public List<string>? Categories { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("icon_url")]
        public string? UconURL { get; set; }

        [JsonProperty("id")]
        public string? JokeId { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string? URL { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}