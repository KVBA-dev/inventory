using System.Text.Json.Serialization;

namespace Inventory;

public sealed class MaterialComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "material";

    private readonly Identifier id;
    [JsonPropertyName("id")]
    public string Id => id.ToString();

    public MaterialComponent(Identifier id) {
        this.id = id;
    }

    [JsonConstructor]
    public MaterialComponent(string id) {
        this.id = Identifier.FromString(id);
    }

    public override Component Clone() => new MaterialComponent(id);

}
