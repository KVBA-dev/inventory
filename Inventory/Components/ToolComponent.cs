using System.Text.Json.Serialization;

namespace Inventory;

public sealed class ToolComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "tool";

    public int tier { get; private set; }
    private readonly List<Tag> canGather;
    [JsonPropertyName("can_gather")]
    public List<string> CanGather => canGather.Select(t => t.ToString()).ToList();

    public ToolComponent(int tier) {
        this.tier = tier;
        canGather = new();
    }

    [JsonConstructor]
    public ToolComponent(int tier, List<string> canGather) {
        this.tier = tier;
        this.canGather = Tag.FromStrings(canGather);
    }

    public ToolComponent AddTag(Tag tag) {
        canGather.Add(tag);
        return this;
    }

    public ToolComponent AddTag(string tag) {
        canGather.Add(Tag.FromString(tag));
        return this;
    }

    public override Component Clone() {
        return new ToolComponent(tier, canGather.Select(t => t.ToString())
            .ToList());
    }
}
