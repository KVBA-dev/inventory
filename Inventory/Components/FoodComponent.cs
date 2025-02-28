using System.Text.Json.Serialization;

namespace Inventory;

public sealed class FoodComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "food";

    [JsonPropertyName("health")]
    public int restoresHealth { get; private set; } = 0;
    [JsonPropertyName("hunger")]
    public int restoresHunger { get; private set; } = 0;

    public FoodComponent(int health, int hunger) {
        restoresHealth = health;
        restoresHunger = hunger;
    }

    public override Component Clone() => new FoodComponent(restoresHealth, restoresHunger);
}
