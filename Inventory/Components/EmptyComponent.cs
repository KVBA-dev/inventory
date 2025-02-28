using System.Text.Json.Serialization;

namespace Inventory;

// use this as a starting point for new components
public sealed class EmptyComponent : Component {
    [JsonPropertyName("type")]
    public override string Name => "name";
    // put your properties here
    // keep them as properties so that they can be serialised easily
    // ---------------------------

    // ---------------------------
    public EmptyComponent() {
    }

    public override Component Clone() => new EmptyComponent();
}
