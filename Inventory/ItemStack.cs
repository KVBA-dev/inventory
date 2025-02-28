namespace Inventory;

public class ItemStack {
    public static readonly ItemStack Empty = new(Item.Empty, 0);

    private readonly List<Component> components;

    public readonly Item item;
    public int count;

    public string Id => item.Id;
    public int Count => count;

    public ItemStack(Item item, int count) {
        this.count = count;
        this.item = item;
        this.components = item.Components.ConvertAll(c => c.Clone());
    }

    public bool CompatibleWith(ItemStack other) {
        return this.item == other.item;
    }
}
