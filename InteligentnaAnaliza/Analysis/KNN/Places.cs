public static class Places
{
    public static readonly string WestGermany = "west-germany";
    public static readonly string USA = "usa";
    public static readonly string France = "france";
    public static readonly string UK = "uk";
    public static readonly string Canada = "canada";
    public static readonly string Japan = "japan";

    public static IEnumerable<string> All => new[] { WestGermany, USA, France, UK, Canada, Japan };
    public static IEnumerable<string> Except(string tag) => (
            All.Where(t => !t.Equals(tag))
        );

    public static bool IsValidTag(string tag) => All.Any(t => t.Equals(tag));
}