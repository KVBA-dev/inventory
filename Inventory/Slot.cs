using System.Text.Json.Serialization;
namespace Inventory;

public enum SlotType {
    Input,
    Output,
    InputOutput,
}

public class Slot {
    private readonly List<IDataLabel> accepts;
    public int index {get;set;}
    public ItemStack stack = ItemStack.Empty;

    [JsonPropertyName("accepts")]
    public List<string> Accepts => accepts.Select(t => t.ToString()!).ToList();

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SlotType type {get;set;}

    [JsonPropertyName("requires_component")]
    public List<string> requiresComponent {get;}

    public Slot() {
        accepts = new();
        requiresComponent = new();
        index = 0;
        type = SlotType.InputOutput;
    }

    [JsonConstructor]
    public Slot(int index, SlotType type, List<string> accepts, List<string> requiresComponent) {
        this.index = index;
        this.type = type;
        this.accepts = IDataLabel.FromStrings(accepts);
        this.requiresComponent = requiresComponent;
    }

    public Slot AtIndex(int index) {
        this.index = index;
        return this;
    }

    public Slot Accept(string tag) => Accept(IDataLabel.FromString(tag));

    public Slot Accept(IDataLabel tag) {
        accepts.Add(tag);
        return this;
    }

    public Slot Accept(params IDataLabel[] tags) {
        foreach (IDataLabel dl in tags) {
            Accept(dl);
        }
        return this;
    }

    public Slot Accept(params string[] tags) => Accept(IDataLabel.FromStrings(tags).ToArray());

    public Slot WithType(SlotType type) {
        this.type = type;
        return this;
    }

    public Slot RequiresComponent<T>() where T: Component {
        requiresComponent.Add(ComponentRegistry.Reg.Single(kv => kv.Value == typeof(T)).Key);
        return this;
    }

    public Slot Clone() {
        Slot s = new() {
            index = index,
            type = type,
        };
        accepts.ForEach(t => s.accepts.Add(t));
        return s;
    }

    public ItemStack AddStack(ItemStack input) {
        if (stack == ItemStack.Empty) {
            stack = input;
            return ItemStack.Empty;
        }

        if (input.CompatibleWith(stack)) {
            if (!input.item.TryGetComponent<StackSizeComponent>(out var size)) {
                return input;
            }
            stack.count += input.count;
            if (stack.count > size.maxSize) {
                input.count = stack.count - size.maxSize;
                stack.count = size.maxSize;
                return input;
            }
            return ItemStack.Empty;
        }

        return input;
    }

    public ItemStack RetrieveStack(int count) {
        Assert.That(count >= 0, "Attempted to retrieve negative amount");
        if (count == 0) {
            return ItemStack.Empty;
        }
        int stackCount = stack.count;
        ItemStack output = new(stack.item, count);
        stack.count -= count;
        if (stack.count <= 0) {
            stack = ItemStack.Empty;
            output.count = stackCount;
        }
        return output;
    }
}