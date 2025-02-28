using System.Text.Json.Serialization;
namespace Inventory;

public class Item : ISerializable {
    private Identifier id;
    private readonly List<Tag> tags;
    private readonly List<Component> components;

    public static readonly Item Empty = new Item(Identifier.Empty).Register();

    [JsonPropertyName("id")]
    public string Id => id.ToString();

    [JsonPropertyName("tags")]
    public List<string> Tags => tags.Select(t => t.ToString()).ToList();

    [JsonPropertyName("components")]
    public List<Component> Components => components;

    public Item() {
        id = default(Identifier);
        tags = new();
        components = new();
    }

    [JsonConstructor]
    public Item(string id, List<string> tags, List<Component> components) {
        this.id = Identifier.FromString(id);
        this.tags = Tag.FromStrings(tags);
        this.components = components;
    }

    public Item(string id) {
        this.id = Identifier.FromString(id);
        tags = new();
        components = new();
    }

    public Item(Identifier id) {
        this.id = id;
        tags = new();
        components = new();
    }

    public Item WithId(Identifier id) {
        this.id = id;
        return this;
    }

    public Item WithId(string id) {
        this.id = Identifier.FromString(id);
        return this;
    }

    public Item WithTag(Tag tag) {
        this.tags.Add(tag);
        return this;
    }

    public Item WithTag(string tag) {
        this.tags.Add(Tag.FromString(tag));
        return this;
    }

    public Item WithTags(IEnumerable<Tag> tags) {
        foreach (Tag t in tags) {
            this.tags.Add(t);
        }
        return this;
    }

    public Item WithComponent(Component component) {
        components.Add(component);
        return this;
    }

    public Item WithComponents(IEnumerable<Component> components) {
        foreach (Component c in components) {
            this.components.Add(c);
        }
        return this;
    }

    public string Serialize() => JsonUtils.Serialize(this);

    public T? GetComponent<T>() where T : Component {
        return (T?)components.SingleOrDefault(c => c is T);
    }

    public bool TryGetComponent<T>(out T component) where T : Component {
        if (HasComponent<T>()) {
            component = GetComponent<T>()!;
            return true;
        }
        component = default(T)!;
        return false;
    }

    public bool HasComponent<T>() where T : Component {
        return components.Any(c => c is T);
    }

    public bool HasComponent(string name) {
        return components.Any(c => c.Name == name);
    }

    public Item Register() {
        GlobalRegistry.Items.Register(this);
        return this;
    }
}
