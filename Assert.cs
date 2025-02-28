public class Assertion : Exception {
    public Assertion(string msg) : base(msg) {}
}

public static class Assert {
    public static void That(bool cond, string errMsg) {
        if (!cond) {
            throw new Assertion($"Assertion failed: {errMsg}");
        }
    }
}