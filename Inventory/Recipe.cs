using System.Text.Json.Serialization;

namespace Inventory;

public class Recipe : ISerializable {
    private Identifier containerId;
    public string container => containerId.ToString();

    [JsonIgnore] public string Id => Identifier.Empty.ToString();

    private readonly List<RecipeItem> inputs;
    [JsonPropertyName("inputs")]
    public List<RecipeItem> Inputs => inputs;

    private readonly List<RecipeItem> outputs;
    [JsonPropertyName("outputs")]
    public List<RecipeItem> Outputs => outputs;

    [JsonConstructor]
    public Recipe(string container, List<RecipeItem> inputs, List<RecipeItem> outputs) {
        this.containerId = Identifier.FromString(container);
        this.inputs = inputs;
        this.outputs = outputs;
    }

    public Recipe(string container) {
        this.containerId = Identifier.FromString(container);
        inputs = new();
        outputs = new();
    }

    public Recipe(Identifier container) {
        this.containerId = container;
        inputs = new();
        outputs = new();
    }

    public Recipe AddInput(int index, string id, int count) {
        inputs.Add(new(index, IDataLabel.FromString(id), count));
        return this;
    }

    public Recipe AddInput(int index, IDataLabel id, int count) {
        inputs.Add(new(index, id, count));
        return this;
    }

    public Recipe AddOutput(int index, string id, int count) {
        outputs.Add(new(index, Identifier.FromString(id), count));
        return this;
    }

    public Recipe AddOutput(int index, Identifier id, int count) {
        outputs.Add(new(index, id, count));
        return this;
    }

    public string Serialize() => JsonUtils.Serialize(this);

    public Recipe Register() {
        GlobalRegistry.Recipes.Register(this);
        return this;
    }
}

public class RecipeItem {
    public int index { get; set; }
    public IDataLabel item { get; set; }
    public int count { get; set; }

    public RecipeItem(int index, IDataLabel item, int count) {
        this.index = index;
        this.item = item;
        this.count = count;
    }
}
