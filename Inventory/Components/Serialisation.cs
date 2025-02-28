using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory;

public class ComponentConverter : JsonConverter<Component> {
    public override Component? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader)!;
        JsonElement jsonObject = doc.RootElement;
        Assert.That(jsonObject.TryGetProperty("type", out var nameElement), "No name specified for the component");

        string? name = nameElement.GetString();
        Assert.That(name is not null, "Invalid name for the component");
        return ComponentFactory.CreateComponent(name!, jsonObject);

    }

    public override void Write(Utf8JsonWriter writer, Component value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

public class DataLabelConverter : JsonConverter<IDataLabel> {
    public override IDataLabel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        Assert.That(reader.TokenType == JsonTokenType.String, "Expected string");
        string? s = reader.GetString();
        Assert.That(s is not null, "String is null");
        return IDataLabel.FromString(s!);
    }

    public override void Write(Utf8JsonWriter writer, IDataLabel value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToString());
    }
}

public static class ComponentFactory {
    public static Component CreateComponent(string name, JsonElement json) {
        Assert.That(ComponentRegistry.Reg.TryGetValue(name, out Type type), $"Unknown component \"{name}\"");
        return (Component)JsonSerializer.Deserialize(json.GetRawText(), type!)!;
    }
}