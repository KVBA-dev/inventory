using System.Text.Json.Serialization;

namespace Inventory;

public sealed class DurabilityComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "durability";

    [JsonPropertyName("max_durability")]
    public int maxDurability { get; private set; }

    public DurabilityComponent(int maxDurability) {
        this.maxDurability = maxDurability;
    }

    public override Component Clone() => new DurabilityComponent(maxDurability);
}
