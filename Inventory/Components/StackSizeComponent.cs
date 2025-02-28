using System.Text.Json.Serialization;

namespace Inventory;

public sealed class StackSizeComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "stack_size";

    [JsonPropertyName("max_size")]
    public int maxSize { get; private set; }

    public StackSizeComponent(int maxSize) {
        this.maxSize = maxSize;
    }

    public override Component Clone() => new StackSizeComponent(maxSize);
}
