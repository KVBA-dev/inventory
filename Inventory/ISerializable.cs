namespace Inventory;

public interface ISerializable {
    public string Id { get; }
    public string Serialize();
}
