using System.Text.Json.Serialization;

namespace Inventory;

public class LootTable : ISerializable {
    private static readonly Random rand = new();
    private Identifier id;
    [JsonPropertyName("id")]
    public string Id => id.ToString();

    private readonly List<LootTableEntry> table;
    [JsonPropertyName("table")]
    public List<LootTableEntry> Table => table;

    public LootTable() {
        id = default(Identifier);
        table = new();
    }

    public LootTable(Identifier id) {
        this.id = id;
        table = new();
    }

    public LootTable(string id) {
        this.id = Identifier.FromString(id);
        table = new();
    }

    [JsonConstructor]
    public LootTable(string id, List<LootTableEntry> table) {
        this.id = Identifier.FromString(id);
        this.table = table;
    }

    public LootTable WithId(Identifier id) {
        this.id = id;
        return this;
    }

    public LootTable WithId(string id) => WithId(Identifier.FromString(id));

    public LootTable Add(Identifier id, int weight, int min, int max) {
        table.Add(new(id, weight, min, max));
        return this;
    }

    public LootTable Add(string id, int weight, int min, int max) => Add(Identifier.FromString(id), weight, min, max);
    public LootTable Add(Identifier id, int weight, int amount) => Add(id, weight, amount, amount);
    public LootTable Add(string id, int weight, int amount) => Add(Identifier.FromString(id), weight, amount);

    public string Serialize() => JsonUtils.Serialize(this);

    public LootTable Register() {
        GlobalRegistry.LootTables.Register(this);
        return this;
    }

    public ItemStack Pick(int lootingLevel = 0) {
        int weights = table.Sum(e => e.weight);
        List<LootTableEntry> newEntries;
        if (lootingLevel == 0) {
            newEntries = table.ConvertAll(e => new LootTableEntry(e.Id, e.weight, e.minAmount, e.maxAmount));

            foreach (LootTableEntry e in newEntries) {
                if (e.weight < weights / 10) {
                    e.weight += lootingLevel;
                    continue;
                }
                e.maxAmount += lootingLevel;
                e.minAmount += lootingLevel;
            }
            weights = newEntries.Sum(e => e.weight);
        }
        else {
            newEntries = table;
        }

        int selectedWeight = rand.Next(weights);

        foreach (LootTableEntry e in newEntries) {
            if (selectedWeight < e.weight) {
                int amount = rand.Next(e.minAmount, e.maxAmount + 1);
                return new(GlobalRegistry.Items[e.Id], amount);
            }
            selectedWeight -= e.weight;
        }
        return ItemStack.Empty;
    }
}

public class LootTableEntry {
    private readonly Identifier id;
    [JsonPropertyName("id")]
    public string Id => id.ToString();
    public int weight { get; set; } = 1;
    [JsonPropertyName("min_amount")]
    public int minAmount { get; set; }
    [JsonPropertyName("max_amount")]
    public int maxAmount { get; set; }

    public LootTableEntry() {
        id = default(Identifier);
    }

    [JsonConstructor]
    public LootTableEntry(string id, int weight, int minAmount, int maxAmount) {
        this.id = Identifier.FromString(id);
        this.weight = weight;
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
    }

    public LootTableEntry(Identifier id, int weight, int minAmount, int maxAmount) {
        this.id = id;
        this.weight = weight;
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
    }
}
