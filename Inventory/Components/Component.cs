using System.Text.Json.Serialization;

namespace Inventory;

public abstract class Component {
    [JsonIgnore] public abstract string Name { get; }
    public abstract Component Clone();
}
