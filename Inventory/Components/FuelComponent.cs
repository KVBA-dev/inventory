using System.Text.Json.Serialization;

namespace Inventory;

public sealed class FuelComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "fuel";
    [JsonPropertyName("fuel_amount")]
    public int fuelAmount { get; set; }
    public FuelComponent(int fuelAmount) {
        this.fuelAmount = fuelAmount;
    }

    public override Component Clone() => new FuelComponent(fuelAmount);
}
