namespace Inventory;

public static class GlobalRegistry {
    public static readonly Registry<Item> Items = new();
    public static readonly Registry<Container> Containers = new();
    public static readonly Registry<LootTable> LootTables = new();
    public static readonly Registry<Recipe> Recipes = new();
}