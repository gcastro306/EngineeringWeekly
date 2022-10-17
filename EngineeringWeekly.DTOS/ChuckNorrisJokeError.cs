using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EngineeringWeekly.DTOS
{
    [ExcludeFromCodeCoverage]
    public class ChuckNorrisJokeError
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public string? Error { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? Date { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }
    }
}