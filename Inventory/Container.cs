using System.Text.Json.Serialization;

namespace Inventory;

public class Container : ISerializable {
    private Identifier id;
    [JsonPropertyName("id")]
    public string Id => id.ToString();

    private readonly List<Slot> slots;
    [JsonPropertyName("slots")]
    public List<Slot> Slots => slots;

    public Container() {
        id = default(Identifier);
        slots = new();
    }

    public Container(Identifier id) {
        this.id = id;
        slots = new();
    }

    public Container(string id) {
        this.id = Identifier.FromString(id);
        slots = new();
    }

    [JsonConstructor]
    public Container(string id, List<Slot> slots) {
        this.id = Identifier.FromString(id);
        this.slots = slots;
    }

    public Container WithId(string id) => WithId(Identifier.FromString(id));

    public Container WithId(Identifier id) {
        this.id = id;
        return this;
    }

    public Container AddSlots(int count, Slot slot) {
        int startCount = slots.Count;
        for (int i = 0; i < count; i++) {
            slots.Add(slot.Clone().AtIndex(startCount + i));
        }
        return this;
    }

    public Container AddSlot(Slot slot) {
        slots.Add(slot.AtIndex(slots.Count));
        return this;
    }

    public string Serialize() => JsonUtils.Serialize(this);

    public Container Register() {
        GlobalRegistry.Containers.Register(this);
        return this;
    }

    public Slot? this[int idx] {
        get => slots.SingleOrDefault(s => s.index == idx);
    }

    public (bool ok, ItemStack remainder) TryPush(int index, ItemStack stack) {
        if (index < 0 || index >= slots.Count) {
            return (false, stack);
        }
        Slot s = this[index]!;
        if (s.type == SlotType.Output) {
            return (false, stack);
        }

        bool matchesTag = s.Accepts.Count == 0;
        if (!matchesTag) {
            foreach (string t in s.Accepts) {
                if (stack.item.Tags.Contains(t)) {
                    matchesTag = true;
                    break;
                }
            }
        }
        if (!matchesTag) {
            return (false, stack);
        }

        if (s.requiresComponent.Count == 0) {
            return (true, s.AddStack(stack));
        }
        foreach (string c in s.requiresComponent) {
            if (stack.item.HasComponent(c)) {
                return (true, s.AddStack(stack));
            }
        }
        return (false, stack);
    }

    public void GenerateLoot(LootTable table) {
        foreach (Slot s in slots) {
            s.stack = table.Pick();
        }
    }
}
