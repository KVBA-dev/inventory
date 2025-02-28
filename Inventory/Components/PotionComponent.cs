using System.Text.Json.Serialization;

namespace Inventory;

public sealed class PotionComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "potion";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PotionEffect effect { get; private set; }
    public int strength { get; private set; }
    public int duration { get; private set; }

    public PotionComponent(PotionEffect effect, int strength, int duration) {
        this.effect = effect;
        this.strength = strength;
        this.duration = duration;
    }

    public override Component Clone() => new PotionComponent(effect, strength, duration);
}
