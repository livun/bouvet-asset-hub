using System.Text.Json;

namespace Bouvet.AssetHub.API.Tests
{
    public class SerializeHelper
    {
        public static string Serialize<T>(T dto)
        {
            return JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public static T Deserialize<T>(string stringContent)
        {
            var result = JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result!;
        }
    }
}
