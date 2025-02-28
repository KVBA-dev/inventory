using System.Text.Json.Serialization;

namespace Inventory;

public sealed class WeaponComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "weapon";

    public int damage { get; private set; }
    public int range { get; private set; }

    public WeaponComponent(int damage, int range) {
        this.damage = damage;
        this.range = range;
    }

    public override Component Clone() => new WeaponComponent(damage, range);
}
