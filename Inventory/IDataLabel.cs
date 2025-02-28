namespace Inventory;

public interface IDataLabel {
    public static IDataLabel FromString(string s) {
        if (s.StartsWith('#')) {
            return Tag.FromString(s);
        }
        return Identifier.FromString(s);
    }

    public static List<IDataLabel> FromStrings(IEnumerable<string> s) {
        return s.Select(FromString).ToList();
    }
}