namespace Inventory;

public static class ComponentRegistry {
    public static readonly Dictionary<string, Type> Reg = new() {
        {"material", typeof(MaterialComponent)},
        {"food", typeof(FoodComponent)},
        {"durability", typeof(DurabilityComponent)},
        {"tool", typeof(ToolComponent)},
        {"weapon", typeof(WeaponComponent)},
        {"potion", typeof(PotionComponent)},
        {"stack_size", typeof(StackSizeComponent)},
        {"fuel", typeof(FuelComponent)},
    };
}