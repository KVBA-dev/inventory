using System.Text.Json;

namespace Inventory;

public static class JsonUtils {
    private static readonly JsonSerializerOptions options = new() {
        Converters = {
            new ComponentConverter(), 
            new DataLabelConverter(),
        },
        WriteIndented = true,
    };

    public static string Serialize<T>(T elem) where T: ISerializable {
        return JsonSerializer.Serialize(elem, options);
    }

    public static T Deserialize<T>(string json) where T: ISerializable {
        return JsonSerializer.Deserialize<T>(json, options)!;
    }
}