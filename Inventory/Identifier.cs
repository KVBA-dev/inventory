using System.Text.Json.Serialization;

namespace Inventory;

public struct Identifier : IDataLabel{
    public readonly string namesp;
    public readonly string id;

    public static readonly Identifier Empty = new Identifier(string.Empty, string.Empty);

    [JsonConstructor]
    public Identifier(string namesp, string id) {
        this.namesp = namesp;
        this.id = id;
    }

    public static Identifier FromString(string id) {
        Assert.That(id is not null, "ID is null");
        string[] t = id!.Split('.');
        Assert.That(t.Length == 2, "Invalid identifier. Must be of format 'namespace.id'");
        return new(t[0], t[1]);

    }

    public override string ToString() {
        return $"{namesp}.{id}";
    }
}

public struct Tag : IDataLabel{
    public readonly string namesp;
    public readonly string tag;

    public Tag(string namesp, string tag) {
        this.namesp = namesp;
        this.tag = tag;
    }

    public static Tag FromString(string tag) {
        Assert.That(tag[0] == '#', "Not a tag. Must be of format '#namespace.tag'");
        string[] t = tag[1..].Split('.');
        Assert.That(t.Length == 2, "Invalid tag. Must be of format '#namespace.tag'");
        return new Tag(t[0], t[1]);

    }

    public static List<Tag> FromStrings(IEnumerable<string> tags) {
        return tags.Select(t => (Tag)Tag.FromString(t)).ToList();
    }

    public override string ToString() {
        return $"#{namesp}.{tag}";
    }
}