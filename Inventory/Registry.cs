namespace Inventory;

public class Registry<T> where T: ISerializable {
    private readonly Dictionary<Identifier, T> reg = new();
    public T this[Identifier id] {
        get => reg[id];
    }

    public T this[string id] => this[Identifier.FromString(id)];

    public void Register(T elem) {
        reg.Add(Identifier.FromString(elem.Id), elem);
    }

    public void Register(params T[] elems) {
        foreach(T e in elems) {
            Register(e);
        }
    }

    public bool LoadAllFromDirectory(string directory, string extension = ".json") {
        if (!Directory.Exists(directory)) {
            return false;
        }

        foreach (string file in Directory.EnumerateFiles(directory)) {
            if (Path.GetExtension(file) != extension) {
                continue;
            }
            Register(JsonUtils.Deserialize<T>(File.ReadAllText(file)));
        }
        return true;
    }

    public IEnumerable<T> Elements() {
        foreach (KeyValuePair<Identifier, T> kv in reg) {
            yield return kv.Value;
        }
    }

    public IEnumerable<Identifier> EnumerateKeys() {
        foreach (Identifier k in reg.Keys) {
            yield return k;
        }
    }
}