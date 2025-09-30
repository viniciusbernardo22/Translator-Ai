using System.Text.Json.Serialization;

namespace Translator_Ai.Infraestructure.Models
{
    public class GetTranslatedJson
    {
        [JsonPropertyName("jsonObject")]
        public required object JsonObject { get; set; }

        [JsonPropertyName("language")]
        public required string Language { get; set; }
    }
}
